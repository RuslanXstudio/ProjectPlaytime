using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    // List of objects whose Rigidbody is to be set to non-kinematic
    public GameObject[] objectsToUnfreeze;

    public AudioSource audioplayer;
    public AudioClip BreakSound;


    public ParticleSystem ParticleEffect;
    public ParticleSystem AdditionalEffects;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag "RaceCar"
        if (other.CompareTag("RaceCar"))
        {
            // Loop through each object in the list
            foreach (GameObject obj in objectsToUnfreeze)
            {
                // Check if the object has a Rigidbody component
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Set isKinematic to false
                    rb.isKinematic = false;


                }
            }
            ParticleEffect.Play();
            if (AdditionalEffects != null)
            {
                AdditionalEffects.Play();

            }

            audioplayer.PlayOneShot(BreakSound, 1.1f);
        }
    }
}