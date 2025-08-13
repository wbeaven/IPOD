using UnityEngine;
using Ami.BroAudio;
using System.Collections;

public class EnemySwarm : MonoBehaviour
{
    [SerializeField] Transform[] drones;
    [SerializeField] float speed = 8f, rotationSpeed = 100f;
    [SerializeField] SoundID buzzsawSFX = default;

    private Transform player;
    private EnemyHealth droneHealth;
    private Vector3 targetDirection;
    private bool detected;
    private bool caught;
    private int destroyedDrones = 0;

    private void Start()
    {
        player = GameObject.Find("Player Container").transform;
    }

    private void Update()
    {
        if (!caught)
        {
            DetectPlayer();
            if (detected)
            {
                PlayerDetected();
            }
        }
        else
        {
            PlayerCaught();
        }
    }

    private void DetectPlayer()
    {
        targetDirection = player.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDirection, out hit, Mathf.Infinity))
        {
            print(hit.transform.name);
            if (hit.transform.CompareTag("Player"))
                detected = true;
        }
        else
        {
            detected = false;
        }
    }

    private void PlayerDetected()
    {
        transform.LookAt(player.position);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        if (Vector3.Distance(transform.position, player.position) < 2f)
        {
            caught = true;
            foreach (var drone in drones)
            {
                drone.GetComponent<EnemySwarmDamage>().isDamaging = true;
            }
            buzzsawSFX.Play();
        }
    }

    private void PlayerCaught()
    {
        transform.position = player.position;
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    public void Destroyed()
    {
        destroyedDrones += 1;
        if (destroyedDrones == drones.Length)
            StartCoroutine(DestructionTimer());
    }

    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(1);
        transform.gameObject.SetActive(false);
    }
}
