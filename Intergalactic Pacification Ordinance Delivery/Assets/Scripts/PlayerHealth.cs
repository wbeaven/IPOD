using Ami.BroAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] SoundID damageSFX = default;
    [SerializeField] GameObject deathEffect;
    [SerializeField] Animator anim;
    [SerializeField] GameMenu gameMenu;

    bool dead;

    public void Update()
    {
        if (health <= 15 && !dead)
        {
            gameMenu.LowHealth();
        }

        if (health <= 0 && !dead)
        {
            dead = true;
            Instantiate(deathEffect, transform);
            gameMenu.GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        BroAudio.Play(damageSFX);
        anim.SetTrigger("damageFlash");
    }

}
