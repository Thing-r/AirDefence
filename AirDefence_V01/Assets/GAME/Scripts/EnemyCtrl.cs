using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public Transform mainBase;

    public float maxSpeed;
    public float rotationSpeed;
    public ShootingPoint[] shootingScript;
    public GameObject destroyExplosion;
    public GameObject smokeTrail;
    public GameObject crashExplosion;
    

    private Transform enemyPos;
    private Rigidbody rigidBody;
    private Vector3 targetPoint;
    private bool attack = true;

    private float enemyHealth = 100;

    void Start()
    {
        enemyPos = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
        mainBase = GameObject.FindGameObjectWithTag("AirBase").transform;
    }

    void Update()
    {
        if (enemyHealth <= 0) return;
        Vector3 target = Vector3.zero;        // Defining target otherwise (else) it's equal 0
        if (attack) target = mainBase.position;
        else target = targetPoint;

        var rot = enemyPos.rotation;
        enemyPos.LookAt(target);
        enemyPos.rotation = Quaternion.RotateTowards(rot, enemyPos.rotation, rotationSpeed * Time.deltaTime);
        ResetTarget();

        RaycastHit hit;
        if (Physics.Raycast(enemyPos.position, enemyPos.forward, out hit, 350f) && hit.collider.tag == "AirBase")
        {
            foreach (var b in shootingScript) b.Shoot();
        }
    }

    void FixedUpdate()
    {
        if (enemyHealth <= 0) maxSpeed -= .1f;

        float currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        if (currentSpeed < maxSpeed)
        {
            rigidBody.velocity = enemyPos.forward * maxSpeed;
        }
    }

    public void ApplyDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0) 
            Destroy();
    }

    private void Destroy()
    {
        rigidBody.useGravity = true;
        var explosion = Instantiate(destroyExplosion, enemyPos.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 5);

        smokeTrail.SetActive(true);
       //smokeTrail.transform.SetParent(null);
        //Destroy(smokeTrail, 6);

        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 7);  

        //EnemyAircraftSpawner.Instance.attackers.Remove(enemyPos);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        { 
        ContactPoint contact = collision.contacts[0];            // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        GameObject expl = Instantiate(crashExplosion, position, rotation);
            Destroy(expl, 1);
            Destroy(gameObject, 1f);
        }
    }

    private void ResetTarget()
    {
        if (targetPoint != Vector3.zero)
        {
            if (Vector3.Distance(targetPoint, enemyPos.position) < 100)
            {
                attack = true;
                targetPoint = Vector3.zero;
            }
        }
        else
        {
            if (Vector3.Distance(mainBase.position, enemyPos.position) < 200)
            {
                attack = false;
                targetPoint = mainBase.position - enemyPos.forward * 500;
            }
        }
    }
}
