using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foostepsounds : MonoBehaviour
{
    public AudioClip footstep;
    public AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void footstepsounds()
    {
        audiosource.PlayOneShot(footstep, 1.0f);
    }
}
