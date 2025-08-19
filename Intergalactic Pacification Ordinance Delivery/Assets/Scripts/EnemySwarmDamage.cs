using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmDamage : MonoBehaviour
{
    [SerializeField] float damage = 1f, damageInterval = 1f, orbitSpeed = 50f;

    public bool isDamaging = false;
    private Transform player;
    private EnemyHealth enemyHealth;
    private Quaternion rot;

    private void Start()
    {
        player = GameObject.Find("Player Container").transform;
        enemyHealth = GetComponent<EnemyHealth>();
        rot = Random.rotation;
    }

    void Update()
    {
        if (enemyHealth.health > 0 && isDamaging)
        {
            DealDamage();
        }
        transform.LookAt(player.position);
        transform.parent.Rotate(Quaternion.ToEulerAngles(rot) * orbitSpeed * Time.deltaTime);
    }

    private void DealDamage()
    {
        isDamaging = false;
        StartCoroutine(DamageTick());
    }

    private IEnumerator DamageTick()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        yield return new WaitForSeconds(damageInterval);
        isDamaging = true;
    }
}
