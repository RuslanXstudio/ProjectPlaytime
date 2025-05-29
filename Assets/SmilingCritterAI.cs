using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SmilingCritterAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject playerObject; // Reference to the player's transform
    public GameObject footsteps;
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Animator Monsteranimator;
    public Vector3 previousPosition;
    public bool isJumpscaring;
    private float defaultSpeed;
    private bool isScared;
    private Quaternion initialRotation; // To store the initial rotation

    public GameObject audio;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        if (!player)
        {
            Debug.LogError("Player reference not set!"); // Log an error if the player reference is not set in the Unity Inspector
        }
        previousPosition = transform.position;
        defaultSpeed = agent.speed; // Save the default speed
    }

    void Update()
    {
        if (!isJumpscaring && !isScared)
        {
            agent.SetDestination(player.position); // Set the destination of the NavMeshAgent to the player's position

            if (transform.position != previousPosition)
            {
                Monsteranimator.SetBool("isChasing", true);
            }
            else
            {
                Monsteranimator.SetBool("isChasing", false);
            }
        }

        if (isScared)
        {
            // Lock rotation by setting it to the initial rotation every frame
            transform.rotation = initialRotation;
        }
    }

    void LateUpdate()
    {
        previousPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isJumpscaring = true;
            Monsteranimator.SetTrigger("Jumpscare");
            playerObject.SetActive(false);
            footsteps.SetActive(false);
        }
    }

    public void scared()
    {
        StartCoroutine(ScaredCoroutine());
        audio.SetActive(true);
    }

    private IEnumerator ScaredCoroutine()
    {
        isScared = true; // Indicate that the critter is scared and should not chase the player
        initialRotation = transform.rotation; // Save the initial rotation

        // Stop following the player and move backwards
        agent.SetDestination(transform.position - transform.forward * 3); // Move backward
        agent.speed = 5; // Increase speed

        yield return new WaitForSeconds(2); // Wait for 3 seconds

        // Reset target and speed
        agent.speed = defaultSpeed;
        isScared = false; // End the scared state
        agent.SetDestination(player.position); // Reset destination to the player
        isJumpscaring = false; // Allow chasing again

        audio.SetActive(false);
    }

}
