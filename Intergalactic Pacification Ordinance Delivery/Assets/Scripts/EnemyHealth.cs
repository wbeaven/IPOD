using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    public float health = 100f;
    bool alive = true;

    private void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            GetComponent<SphereCollider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            GetComponent<EnemyShooting>().bullet.gameObject.SetActive(false);
            GetComponent<EnemyShooting>().enabled = false;
            Instantiate(explosion, transform);
        }
    }

    public void Damage(float dmg)
    {
        health -= dmg;
    }
}
