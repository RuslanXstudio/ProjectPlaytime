using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject objectToActivate; // O objeto que será ativado/desativado
    public AudioSource audioSource; // A fonte do som
    public AudioClip clip; // O som que será tocado

    private bool isActive = false; // Controla se o objeto está ativo ou não

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive; // Inverte o estado de isActive

            objectToActivate.SetActive(isActive); // Ativa/desativa o objeto

            if (isActive)
            {
                audioSource.PlayOneShot(clip, 2.0f); // Toca o som
            }
        }
    }
}