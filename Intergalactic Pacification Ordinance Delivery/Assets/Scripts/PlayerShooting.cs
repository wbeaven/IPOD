using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Ami.BroAudio;
using static UnityEngine.GraphicsBuffer;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] float mainCannonDmg = 10f;
    [SerializeField] float cooldownTime = 0.5f;
    private GameObject hitFX;
    [SerializeField] Image leftCooldownIcon, rightCooldownIcon;
    //[SerializeField] Transform leftMuzzle, rightMuzzle;
    [SerializeField] GameObject leftMuzzleFX, rightMuzzleFX;
    [SerializeField] SoundID cannonAudio = default;
    public bool paused;

    private bool canLeftShoot = true, canRightShoot = true;
    private Transform hitObject;
    private Vector3 hitPos;

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
            StartCoroutine(MuzzleTimer(leftMuzzleFX));
            BroAudio.Play(cannonAudio);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                hitPos = hit.point;
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
            StartCoroutine(MuzzleTimer(rightMuzzleFX));
            BroAudio.Play(cannonAudio);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                hitPos = hit.point;
                hitObject = hit.transform;
                HitTarget();
            }
        }
    }

    private void HitTarget()
    {
        //hitFX = ObjectPooler.Instance.GetPooledObject();
        hitFX = ObjectPool.SharedInstance.GetPooledObject("hitFX");
        if (hitFX != null)
        {
            hitFX.transform.position = hitPos;
            StartCoroutine(HitFXTimer(hitFX));
        }

        if (hitObject.CompareTag("Destructible"))
        {
            IDamageable damageable = hitObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(mainCannonDmg);
            }
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
    
    private IEnumerator HitFXTimer(GameObject fx)
    {
        fx.SetActive(true);
        yield return new WaitForSeconds(1f);
        fx.SetActive(false);
    }
}
