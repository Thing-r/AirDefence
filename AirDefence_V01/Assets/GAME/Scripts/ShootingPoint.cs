using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{

    public GameObject shellPrefab;
    public GameObject shootEffect;
    public float cooldownMax = .2f;

    public new Transform camera;
    private bool isOnCoolDown;

    void Start()
    {
        camera = Camera.main.transform;
    }

    public void Shoot()
    {
        if (isOnCoolDown) return;

        GameObject effect = Instantiate(shootEffect, transform.position, Quaternion.identity) as GameObject;
        effect.transform.parent = transform;
        Destroy(effect, .1f);

        GameObject bullet = Instantiate(shellPrefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 180, ForceMode.Impulse);

        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit))
        {
            bullet.transform.LookAt(hit.point);
        }

        isOnCoolDown = true;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(.1f, cooldownMax));
        isOnCoolDown = false;
    }
}
