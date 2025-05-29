using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayer : MonoBehaviour
{
    public GameObject player;
    public CameraControl camcontroller;
    public GameObject TurretCamera;

    public GameObject SPECFICCANVAS;
    public GameObject SPECFICEXITCANVAS;

    public void DeactivatePlayer()
    {
        player.SetActive(false);
        camcontroller.enabled = true;
        TurretCamera.SetActive(true);
        SPECFICEXITCANVAS.SetActive(false);

    }
    public void activatePlayer()
    {
        player.SetActive(true);
        camcontroller.enabled = false;
        TurretCamera.SetActive(false);
        SPECFICCANVAS.SetActive(false);

    }
}
