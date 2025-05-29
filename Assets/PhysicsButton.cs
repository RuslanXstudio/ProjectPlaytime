using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsButton : MonoBehaviour
{
    public Animator buttonAnimator;
    public Animator Door;
    public GameObject OptionalObjectToEnable;

    public AudioSource audiosource;
    public AudioClip buttonClickSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physics"))
        {
            buttonAnimator.SetBool("push", true);
            Door.SetBool("Open", true);
            audiosource.PlayOneShot(buttonClickSound, 8f);

            if (OptionalObjectToEnable != null)
            {
                OptionalObjectToEnable.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Physics"))
        {
            buttonAnimator.SetBool("push", false);
            Door.SetBool("Open", false);
            audiosource.PlayOneShot(buttonClickSound, 3f);

            if (OptionalObjectToEnable != null)
            {
                OptionalObjectToEnable.SetActive(false);
            }
        }
    }
}
