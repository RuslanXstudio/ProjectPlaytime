using UnityEngine;
using UnityEngine.AI;

public class FaceTowardsObject : MonoBehaviour
{
    public Transform target;  // The target GameObject to face
    public NavMeshAgent navMeshAgent;

    void Start()
    {
        // Get the NavMeshAgent component
    }

    void Update()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Calculate the rotation needed to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply the rotation to this GameObject
            transform.rotation = targetRotation;

            // Optionally, you can update the NavMeshAgent's destination here if needed
            // navMeshAgent.SetDestination(target.position);
        }
    }

    void LateUpdate()
    {
        // Ensure the NavMeshAgent doesn't override the rotation
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Calculate the rotation needed to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Apply the rotation to this GameObject
            transform.rotation = targetRotation;
        }
    }
}
