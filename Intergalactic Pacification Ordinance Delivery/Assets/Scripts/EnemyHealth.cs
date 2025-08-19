using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject visuals;

    public float health = 100f;
    bool alive = true;
    EnemyShooting enemyShooting;
    EnemySwarm enemySwarm;

    private void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        enemySwarm = transform.parent.GetComponentInParent<EnemySwarm>();
    }

    private void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            GetComponent<SphereCollider>().enabled = false;
            visuals.SetActive(false);
            if (enemyShooting != null)
            {
                GetComponent<EnemyShooting>().bullet.gameObject.SetActive(false);
                GetComponent<EnemyShooting>().enabled = false;
            }
            if (enemySwarm != null)
            {
                enemySwarm.Destroyed();
            }
            Instantiate(explosion, transform);
        }
    }

    public void Damage(float dmg)
    {
        health -= dmg;
    }
}
