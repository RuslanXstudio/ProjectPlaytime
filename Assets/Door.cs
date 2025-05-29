using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isopen = false;
    public Animator anim;
    public AudioSource AudioPlayer;
    public AudioClip doorsounds;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isopen)
        {
            anim.SetBool("OpenDoor", true);
        }
        if (!isopen)
        {
            anim.SetBool("OpenDoor", false);
        }
    }

    public void Interact()
    {
        isopen = !isopen;
        AudioPlayer.PlayOneShot(doorsounds, 0.6f);
       
    }
}
