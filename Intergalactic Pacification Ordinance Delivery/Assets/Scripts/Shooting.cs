using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Ami.BroAudio;

public class Shooting : MonoBehaviour
{
    [SerializeField] float mainCannonDmg = 10f;
    [SerializeField] float cooldownTime = 0.5f;
    [SerializeField] Transform explosion; // Temp hit animation
    [SerializeField] Image leftCooldownIcon, rightCooldownIcon;
    [SerializeField] Transform leftMuzzle, rightMuzzle;
    [SerializeField] GameObject leftMuzzlePrefab, rightMuzzlePrefab;
    [SerializeField] SoundID cannonAudio = default;
    public bool paused;

    bool canLeftShoot = true, canRightShoot = true;
    Transform hitObject;

    private void Update()
    {
        Cannons();

        if (!canLeftShoot)
        {
            leftCooldownIcon.fillAmount += Time.deltaTime * 2;
        }
        if (!canRightShoot)
        {
            rightCooldownIcon.fillAmount += Time.deltaTime * 2;
        }
    }

    private void Cannons()
    {
        if (paused)
            return;

        if (Input.GetMouseButtonDown(0) && canLeftShoot)
        {
            canLeftShoot = false;
            StartCoroutine(LeftCannonCooldown());
            leftCooldownIcon.fillAmount = 0f;      
            //Instantiate(leftMuzzlePrefab, leftMuzzle);
            StartCoroutine(MuzzleTimer(leftMuzzlePrefab));
            BroAudio.Play(cannonAudio);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                hitObject = hit.transform;
                HitTarget();
            }
        }

        if (Input.GetMouseButtonDown(1) && canRightShoot)
        {
            canRightShoot = false;
            StartCoroutine(RightCannonCooldown());
            rightCooldownIcon.fillAmount = 0f;
            //Instantiate(rightMuzzlePrefab, rightMuzzle);
            StartCoroutine(MuzzleTimer(rightMuzzlePrefab));
            BroAudio.Play(cannonAudio);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                hitObject = hit.transform;
                HitTarget();
            }
        }
    }

    private void HitTarget()
    {
        //explosion.gameObject.SetActive(true);
        //explosion.GetComponent<MeshRenderer>().enabled = true;
        //explosion.transform.position = hit.point;
        if (hitObject.CompareTag("Destructible"))
        {
            if (hitObject.GetComponent<Destructible>() != null)
                hitObject.GetComponent<Destructible>().Damage(mainCannonDmg);
            else if (hitObject.GetComponent<EnemyHealth>() != null)
                hitObject.GetComponent<EnemyHealth>().Damage(mainCannonDmg);
        }
    }

    private IEnumerator LeftCannonCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canLeftShoot = true;
    }

    private IEnumerator RightCannonCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canRightShoot = true;
    }

    private IEnumerator MuzzleTimer(GameObject muzzle)
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzle.SetActive(false);
    }

}
