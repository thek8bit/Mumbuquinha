using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configura��es")]
    public Transform player;  // Refer�ncia para o jogador
    public float smoothSpeed = 0.125f;  // Velocidade de suaviza��o do movimento
    public Vector3 offset;  // Dist�ncia fixa da c�mera em rela��o ao jogador

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            // A c�mera sempre segue o jogador com a posi��o ajustada pelo 'offset'
            Vector3 desiredPosition = player.position + offset;

            // Suaviza a transi��o da posi��o da c�mera para a posi��o desejada
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Atualiza a posi��o da c�mera
            transform.position = smoothedPosition;

            // A c�mera deve manter a rota��o fixa (n�o a segue)
            // Mant�m o �ngulo fixo da c�mera sem alterar
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}