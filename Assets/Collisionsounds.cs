using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisionsounds : MonoBehaviour
{
    public AudioSource soundplayer;
    public AudioClip Collision1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter (Collision collision)
    {
        soundplayer.PlayOneShot(Collision1, 1.0f);
        
    }
}
