using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToCar : MonoBehaviour
{
    public Animator Car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drive()
    {
        Car.SetTrigger("Open");
    }
}
