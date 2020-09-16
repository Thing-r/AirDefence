using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public bool isEnemy;
    public float enemyDamage = 1f;
    void Start()
    {
        //belongsTo = GameObject.FindGameObjectWithTag("Enemy").transform;
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!isEnemy && col.tag == "Enemy")
        {
            col.GetComponent<EnemyCtrl>().ApplyDamage(100f);
        }
        else if (isEnemy && col.tag == "AirBase")
        {
            col.GetComponent<AlienBase>().ApplyDamage(enemyDamage);
        }
        Destroy(gameObject);
    }
}
