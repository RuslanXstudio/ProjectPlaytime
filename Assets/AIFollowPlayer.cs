using UnityEngine;
using UnityEngine.AI;

public class AIFollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject playerObject; // Reference to the player's transform
    public GameObject footsteps;
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Animator Monsteranimator;
    public Vector3 previousPosition;
    public bool isJumpscaring;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        if (!player)
        {
            Debug.LogError("Player reference not set!"); // Log an error if the player reference is not set in the Unity Inspector
        }
        previousPosition = transform.position;

    }

    void Update()
    {
        if (!isJumpscaring)
        {
            agent.SetDestination(player.position); // Set the destination of the NavMeshAgent to the player's position

            if (transform.position != previousPosition)
            {
                Monsteranimator.SetBool("isChasing", true);

            }

            if (transform.position == previousPosition)
            {
                Monsteranimator.SetBool("isChasing", false);

            }
        }





    }

    void LateUpdate()
    {
        previousPosition = transform.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            isJumpscaring = true;
            Monsteranimator.SetTrigger("Jumpscare");
            playerObject.SetActive(false);
            footsteps.SetActive(false);

        }
    }


}
