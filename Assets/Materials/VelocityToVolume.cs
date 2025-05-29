using UnityEngine;

public class VelocityToVolume : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource audioSource;
    public float maxVelocity = 10f;
    public float maxVolume = 1f;

    void Update()
    {
        if (rb != null && audioSource != null)
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
            audioSource.volume = mappedVolume;
        }
    }
}
