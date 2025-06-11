using UnityEngine;

public class CameraFollowOrbit : MonoBehaviour
{
    [Header("Refer�ncia")]
    public Transform target; // O jogador

    [Header("Configura��o da C�mera")]
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 3.0f;

    [Header("Limites de rota��o vertical")]
    public float minY = -20f;
    public float maxY = 60f;

    [Header("Camadas que a c�mera deve colidir")]
    public LayerMask collisionLayers;

    private float currentX = 0f;
    private float currentY = 20f;

    void Start()
    {
        // Trava e esconde o cursor do mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Entrada do mouse
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minY, maxY);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredDir = rotation * new Vector3(0, 0, -distance);
        Vector3 targetCenter = target.position + Vector3.up * height;
        Vector3 desiredPosition = targetCenter + desiredDir;

        // Raycast para verificar colis�o
        RaycastHit hit;
        if (Physics.Raycast(targetCenter, desiredDir.normalized, out hit, distance, collisionLayers))
        {
            // Posiciona a c�mera no ponto de colis�o (com leve recuo)
            transform.position = hit.point - desiredDir.normalized * 0.1f;
        }
        else
        {
            transform.position = desiredPosition;
        }

        // Olhar para o jogador
        transform.LookAt(targetCenter);
    }
}