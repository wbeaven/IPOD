using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySwarm : MonoBehaviour
{
    [SerializeField] Transform[] drones;
    [SerializeField] float speed = 5f;
    private Transform player;
    private Vector3 targetDirection;
    private LayerMask playerLayer;
    private bool detected;
    private bool caught;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        playerLayer = LayerMask.NameToLayer("Player");
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

        if (Vector3.Distance(transform.position, player.position) < 0.001f)
        {
            caught = true;
        }
    }

    private void PlayerCaught()
    {
        transform.position = player.position;
        // make drones orbit around
        // start buzzing sound effects
        // start applying damage for each drone alive
    }
}
