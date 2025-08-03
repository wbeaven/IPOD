using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] float force = 100f, radius = 100f;
    [SerializeField] Rigidbody[] cells;
    [SerializeField] Transform[] fissures;
    [SerializeField] GameObject explosion;

    GameMenu gameMenu;
    int health = 0;
    bool alive = true;
    GameObject planet, cellParent;

    void Start()
    {
        planet = transform.GetChild(0).gameObject;
        cellParent = transform.GetChild(1).gameObject;
        gameMenu = GameObject.Find("Level Manager").GetComponent<GameMenu>();
    }

    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            planet.SetActive(false);
            cellParent.SetActive(true);
            StartCoroutine(Effect());
            gameMenu.Win();
        }
    }

    public void AddPlanetHealth()
    {
        health++;
    }

    public void RemovePlanetHealth()
    {
        health--;
    }

    private IEnumerator Effect()
    {
        yield return new WaitForSeconds(1f);
        foreach (Rigidbody rb in cells)
        {
            rb.AddExplosionForce(force, cellParent.transform.position, radius);
        }
        foreach (Transform tf in fissures)
        {
            Instantiate(explosion, tf);
        }
        yield return new WaitForSeconds(3f);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
