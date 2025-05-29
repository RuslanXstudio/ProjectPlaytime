using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceLine : MonoBehaviour
{
    public AudioSource soundplayer;
    public AudioClip Voiceline;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio()
    {
        soundplayer.PlayOneShot(Voiceline, 1.0f);
    }
}
