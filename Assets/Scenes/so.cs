using UnityEngine;

public class so : MonoBehaviour
{
    public float balanceIntensity = 0.5f;
    public float inclineIntensity = 0.5f;
    public float balanceSpeed = 10f; // Velocidade do balanceamento
    public float tiltSpeed = 5f; // Velocidade da inclinação
    public float leanAngle = 15f; // Maximum angle to lean
    public GameObject mainCamera; // Reference to the main camera
    public float cameraLeanFactor = 0.5f; // Factor to lean the camera less than the object

    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private Quaternion currentBalanceRotation;
    private Quaternion currentTiltRotation;
    private Quaternion cameraOriginalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
        originalPosition = transform.localPosition;
        currentBalanceRotation = originalRotation;
        currentTiltRotation = originalRotation;

        // Store the camera's original rotation
        if (mainCamera != null)
        {
            cameraOriginalRotation = mainCamera.transform.localRotation;
        }
    }

    void Update()
    {
        SwingObject();
        InclineObject();
        LeanCamera();
    }

    void SwingObject()
    {
        // Controle da rotação com base no mouse
        float mouseX = Input.GetAxis("Mouse X") * balanceIntensity;
        float mouseY = Input.GetAxis("Mouse Y") * balanceIntensity;
        Quaternion targetRotation = Quaternion.Euler(-mouseY, mouseX, 0);

        // Suaviza a rotação usando Lerp
        currentBalanceRotation = Quaternion.Lerp(currentBalanceRotation, targetRotation, Time.deltaTime * balanceSpeed);
        transform.localRotation = currentBalanceRotation;
    }

    void InclineObject()
    {
        // Inclinação ao longo do eixo Z com base no input horizontal
        float moveX = Input.GetAxis("Horizontal") * inclineIntensity;

        // Incline the object based on A/D keys (lean left/right)
        float lean = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            lean = leanAngle; // Lean to the right
        }
        else if (Input.GetKey(KeyCode.D))
        {
            lean = -leanAngle; // Lean to the left
        }

        Quaternion tiltRotation = Quaternion.Euler(0, 0, moveX * 30f + lean);

        // Suaviza a rotação de inclinação
        currentTiltRotation = Quaternion.Lerp(currentTiltRotation, tiltRotation, Time.deltaTime * tiltSpeed);

        // Aplica a rotação de inclinação mantendo a rotação de balanceamento
        transform.localRotation = Quaternion.Euler(
            currentBalanceRotation.eulerAngles.x,
            currentBalanceRotation.eulerAngles.y,
            currentTiltRotation.eulerAngles.z
        );

        // Mova o objeto na posição com base no input vertical
        float moveZ = Input.GetAxis("Vertical") * inclineIntensity;
        Vector3 targetPosition = originalPosition + new Vector3(0, 0, moveZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 10);
    }

    void LeanCamera()
    {
        if (mainCamera != null)
        {
            // Calculate the lean for the camera, half of the object's lean
            float cameraLean = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                cameraLean = leanAngle * cameraLeanFactor; // Lean right for the camera
            }
            else if (Input.GetKey(KeyCode.D))
            {
                cameraLean = -leanAngle * cameraLeanFactor; // Lean left for the camera
            }

            Quaternion cameraTiltRotation = Quaternion.Euler(0, 0, cameraLean);

            // Smoothly apply the camera's lean rotation
            mainCamera.transform.localRotation = Quaternion.Lerp(
                mainCamera.transform.localRotation,
                cameraOriginalRotation * cameraTiltRotation,
                Time.deltaTime * tiltSpeed
            );
        }
    }
}
