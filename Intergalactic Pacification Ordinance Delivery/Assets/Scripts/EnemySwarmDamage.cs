using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmDamage : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] float damageInterval = 1f;

    public bool isDamaging = false;
    private Transform player;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        player = GameObject.Find("Player Container").transform;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (enemyHealth.health > 0 && isDamaging)
        {
            StartCoroutine(DamageTick());
            isDamaging = false;
        }
    }

    private IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(damageInterval);
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        StartCoroutine(DamageTick());
    }
}
