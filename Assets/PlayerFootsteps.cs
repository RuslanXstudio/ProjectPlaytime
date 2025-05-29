using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource walksounds;
    public AudioSource SprintSounds;
    public AudioSource CrouchSounds;
    public GameObject SprintAudioSource;
    public float maxVelocity = 10f;
    public float maxVolume = 1f;
    public SimpleFPSController player;

    void Update()
    {
        if (rb != null && walksounds != null)
        {
            if (player.isGrounded == true)
            {
                // Calculate the magnitude of the velocity vector
                float velocityMagnitude = rb.velocity.magnitude;

                // Map velocity magnitude to volume level
                float mappedVolume = Mathf.Lerp(0f, maxVolume, velocityMagnitude / maxVelocity);

                // Double the volume level
                mappedVolume *= 4f;

                // Ensure volume is within range
                mappedVolume = Mathf.Clamp(mappedVolume, 0f, maxVolume * 2f);

                // Set the volume of the audio source
                if ((player.isGrounded == true))
                {
                    if (Input.GetKey(KeyCode.LeftShift) && player.isCrouching == false)
                    {
                        SprintSounds.volume = mappedVolume;
                        walksounds.enabled = false;
                        CrouchSounds.enabled = false;
                        SprintSounds.enabled = true;

                    }
                    if (!Input.GetKey(KeyCode.LeftShift) && player.isCrouching == false)
                    {
                        walksounds.volume = mappedVolume;
                        SprintSounds.enabled = false;
                        CrouchSounds.enabled = false;

                    }
                    if (player.isCrouching == true)
                    {
                        CrouchSounds.volume = mappedVolume;
                        SprintSounds.enabled = false;
                        walksounds.enabled = false;

                    }
                }
            }

        }

        if (player.isGrounded == false)
        {
            SprintAudioSource.SetActive(false);
            walksounds.enabled = false;
            CrouchSounds.enabled = false;
        }
        if (player.isGrounded == true)
        {
            walksounds.enabled = true;
            CrouchSounds.enabled = false;
            SprintAudioSource.SetActive(true);

        }
        if (player.isCrouching == true)
        {
            SprintSounds.enabled = false;
            walksounds.enabled = false;
            CrouchSounds.enabled = true;

        }
    }
}