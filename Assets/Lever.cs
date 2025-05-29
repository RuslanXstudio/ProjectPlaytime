using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;       // Reference to the Animator component
    public string animatorBoolName; // Name of the boolean parameter in the Animator

    private int previousChildCount; // To keep track of the previous number of child objects

    public AudioSource audiosource;
    public AudioClip leverpullSFX;

    public FireHand GrabTurretToConnect;

    void Start()
    {
        // Initialize the previous child count
        previousChildCount = transform.childCount;
    }

    void Update()
    {
        // Check the current number of child objects
        int currentChildCount = transform.childCount;

        // If the number of child objects has increased, toggle the animator boolean
        if (currentChildCount > previousChildCount)
        {
            audiosource.PlayOneShot(leverpullSFX, 1f);

            // Toggle the boolean in the Animator
            if (animator != null)
            {
                bool currentBoolValue = animator.GetBool(animatorBoolName);
                animator.SetBool(animatorBoolName, !currentBoolValue); // Toggle the bool
            }

            // Update the previous child count
            previousChildCount = currentChildCount;
        }
        else if (currentChildCount < previousChildCount)
        {
            // Update the previous child count if it has decreased
            previousChildCount = currentChildCount;
        }
    }

    public void fireTurret()
    {
        GrabTurretToConnect.LaunchTurret();
    }
}
