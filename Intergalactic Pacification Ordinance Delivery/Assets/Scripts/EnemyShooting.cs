using SWS;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform bullet;
    [SerializeField] float damage = 5f, speed = 50f, cooldown = 1f, lifetime = 3f;
    [SerializeField] bool homing;
    [SerializeField] Transform lookAtMesh, baseSwivel, barrelSwivel;

    Transform hitObject, player, follower;
    bool fired, shooting;
    Vector3 currentPos;
    MeshRenderer bulletMesh;

    private void Start()
    {
        follower = GameObject.Find("Player Follower").transform;
        player = GameObject.Find("Player Container").transform;

        bulletMesh = bullet.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hit, Mathf.Infinity);
        hitObject = hit.transform;

        if (hitObject != null && hitObject.CompareTag("Player"))
        {
            lookAtMesh.LookAt(follower);
            if (!fired)
            {
                fired = true;
                StartCoroutine(Shoot());
            }
        }

        //if (shooting && !homing)
        //    Bullet();
        //else if (shooting && homing)
        //    Rocket();        
        if (shooting && !homing)
            Projectile(currentPos);
        else if (shooting && homing)
            Projectile(follower.position);
    }

    private void Projectile(Vector3 target)
    {
        bullet.position = Vector3.MoveTowards(bullet.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(bullet.position, player.position) < 0.5f)
        {
            //bulletMesh.enabled = false;
            bullet.gameObject.SetActive(false);
            shooting = false;
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            // put explosion sfx here
        }
        if (Vector3.Distance(bullet.position, target) < 0.5f)
        {
            //bulletMesh.enabled = false;
            bullet.gameObject.SetActive(false);
            shooting = false;
            // put explosion sfx here
        }
    }

    //private void Bullet()
    //{
    //    bullet.position = Vector3.MoveTowards(bullet.position, currentPos, bulletSpeed * Time.deltaTime);
    //    if (Vector3.Distance(bullet.position, currentPos) < 0.5f)
    //    {
    //        bulletMesh.enabled = false;
    //        // put explosion sfx here
    //    }
    //}

    //private void Rocket()
    //{
    //    bullet.position = Vector3.MoveTowards(bullet.position, follower.position, bulletSpeed * Time.deltaTime);
    //    if (Vector3.Distance(bullet.position, follower.position) < 0.5f)
    //    {
    //        bulletMesh.enabled = false;
    //        // put explosion sfx here
    //    }
    //}

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(cooldown);
        bullet.position = transform.position;
        shooting = true;
        currentPos = player.position;
        //bulletMesh.enabled = true;
        bullet.gameObject.SetActive(true);
        bullet.LookAt(currentPos);
        yield return new WaitForSeconds(lifetime);
        shooting = false;
        fired = false;
        //bulletMesh.enabled = false;
        bullet.gameObject.SetActive(false);
    }
}