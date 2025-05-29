using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHand : MonoBehaviour
{
    public float raycastDistance = 10f;       // Distance the raycast will check for a hit
    public float moveSpeed = 5f;              // Speed at which the object moves to and from the target position
    public KeyCode triggerKey = KeyCode.F;    // Key to trigger the movement (default is F)

    private Vector3 originalPosition;         // The original position of the object
    public bool isMovingToTarget = false;    // Flag for moving to the target position
    public bool isMovingBack = false;        // Flag for moving back to the original position
    private Vector3 targetPosition;           // The target position (raycast hit point or max range)
    public float delayDuration = 0.1f;       // Duration to stay at the target position after hitting something
    private float delayTimer;                  // Timer to manage the delay
    public Transform origin;

    public AudioSource audiosource;
    public AudioClip retractsfx;

    public Animator cable;
    public bool attarget = false;

    public Transform orginCamera;

    public GrabTurretReciever reciever;
    void Start()
    {
        // Store the original position of the object
        originalPosition = origin.position;
    }

    void Update()
    {

        // Handle movement towards the target position
        if (isMovingToTarget)
        {
            // Move linearly towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
                {

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PowerReciever"))
                    {
                        if (hit.collider.gameObject.GetComponent<GrabTurretReciever>() != null)
                        {
                            // If it hit something, start the delay before moving back
                            cable.SetBool("moving", false);
                            attarget = true;

                            reciever = hit.collider.gameObject.GetComponent<GrabTurretReciever>();
                            reciever.OpenDoor();
                        }
                    }
                    else
                    {
                        // If it hit something, start the delay before moving back
                        isMovingToTarget = false;
                        delayTimer = delayDuration; // Set the delay timer
                        attarget = false;

                        cable.SetBool("moving", false);
                    }


                }
                else
                {
                    // Stop moving to the target position and start moving back
                    isMovingToTarget = false;
                    isMovingBack = true;
                    attarget = false;

                    cable.SetBool("moving", false);

                }
            }
        }
        if (attarget)
        {
            RaycastHit hit;
            float distanceToTarget = Vector3.Distance(orginCamera.position, transform.position);

            if (Physics.Raycast(orginCamera.position, transform.forward, out hit, distanceToTarget))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("PowerReciever") && hit.collider.gameObject.layer != LayerMask.NameToLayer("player"))
                {
                    isMovingToTarget = false;
                    isMovingBack = true;
                    attarget = false;

                    cable.SetBool("moving", false);
                }

            }

        }
        // Handle delay after hitting an object
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime; // Decrease the timer
            if (delayTimer <= 0)
            {
                isMovingBack = true; // Start moving back after the delay
            }
        }

        // Handle movement back to the original position
        if (isMovingBack)
        {
            attarget = false;
            if (reciever != null)
            {
                reciever.CloseDoor();

            }

            // Move linearly back to the original position
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the original position
            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                // Stop moving back
                isMovingBack = false;

            }
        }
    }

    public void LaunchTurret()
    {

        if (!isMovingToTarget && !isMovingBack)
        {


            isMovingToTarget = true;
            cable.SetBool("moving", true);

            RaycastHit hit;
            originalPosition = origin.position;

            // Cast a ray forward from the object
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                // If the raycast hits an object, set the target position to the hit point
                targetPosition = hit.point;
            }
            else
            {
                // If the raycast does not hit anything, set the target position to the maximum distance
                targetPosition = transform.position + transform.forward * raycastDistance;
            }

            // Start moving to the target position
            delayTimer = 0; // Reset delay timer
        }
        if (attarget)
        {
            // Stop moving to the target position and start moving back
            isMovingToTarget = false;
            isMovingBack = true;

            cable.SetBool("moving", false);

            attarget = false;
        }

    }
}