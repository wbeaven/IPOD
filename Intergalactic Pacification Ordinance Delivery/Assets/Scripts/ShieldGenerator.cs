using Ami.BroAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour, IDamageable
{
    public float health = 100f;
    [SerializeField] GameObject generatorVisuals, shield;

    private GameObject explosion;
    private bool alive = true;

    private void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            GetComponent<SphereCollider>().enabled = false;
            generatorVisuals.SetActive(false);
            shield.SetActive(false);
            explosion = ObjectPool.SharedInstance.GetPooledObject("enemyDestroyedFX");
            if (explosion != null)
            {
                explosion.transform.position = transform.position;
                StartCoroutine(ExplosionFXTimer(explosion));
            }
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
    }

    private IEnumerator ExplosionFXTimer(GameObject fx)
    {
        fx.SetActive(true);
        yield return new WaitForSeconds(2f);
        fx.SetActive(false);
    }
}
