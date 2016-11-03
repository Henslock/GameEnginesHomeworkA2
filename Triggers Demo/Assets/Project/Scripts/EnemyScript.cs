using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    /*
    Triggers Assignment - Trumpet enemies, also has deteced enemy range

    Josh Bellyk - 100526009
    Owen Meier  - 100538643    
    */
    public Transform player;
    NavMeshAgent agent;

    public GameObject explosionPrefab;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= 7.0f)
        {

            agent.SetDestination(player.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player)
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
