using UnityEngine;

public class FlareCollisionHandler : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the Flare Projectile!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Para o movimento do flare ao colidir com qualquer coisa
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Zera a velocidade
            rb.isKinematic = true; // Faz com que o flare pare no lugar
        }

        // Adicione mais lógica aqui se necessário
    }
}