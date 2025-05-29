using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPuzzle : MonoBehaviour
{
    public Animator door;
    public List<GameObject> PowerPillars;

    public AudioSource audiosource;
    public AudioClip puzzlecomplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void opendoor()
    {
        bool allActive = true;
        foreach (GameObject obj in PowerPillars)
        {
            if (!obj.activeSelf)
            {
                allActive = false;
                break;
            }
        }

        if (allActive)
        {
            door.SetBool("Open", true);

            audiosource.PlayOneShot(puzzlecomplete, 1f);
        }
    }
}
