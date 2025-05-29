using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPulling : MonoBehaviour
{
    private int lastChildCount;
    public Door doorscript;
    void Start()
    {
        // Initialize with the current number of children
        lastChildCount = transform.childCount;
    }

    void Update()
    {
        // Check if the child count has changed
        if (transform.childCount > lastChildCount)
        {
            doorscript.isopen = true;
        }
    }
}