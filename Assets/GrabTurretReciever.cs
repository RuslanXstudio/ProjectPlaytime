using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTurretReciever : MonoBehaviour
{
    public Animator animator;       // Reference to the Animator component
    public string animatorBoolName; // Name of the boolean parameter in the Animator

    void Start()
    {

    }

    public void OpenDoor()
    {
        animator.SetBool(animatorBoolName, true);
    }


    public void CloseDoor()
    {
        animator.SetBool(animatorBoolName, false);
    }
}
