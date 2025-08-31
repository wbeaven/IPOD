using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    [SerializeField] bool criticalTarget;
    [SerializeField] GameObject explosion;
    
    public float health = 100f;
    bool alive = true;
    Planet planet;

    private void Start()
    {
        planet = GameObject.Find("Planet Holder").GetComponent<Planet>();
        if (criticalTarget)
            planet.AddPlanetHealth();
    }

    private void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            GetComponent<SphereCollider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            Instantiate(explosion, transform);
            planet.RemovePlanetHealth();
            //StartCoroutine(Effect());
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
    }
}