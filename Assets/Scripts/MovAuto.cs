using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovAuto : MonoBehaviour
{
    [Header("Configura��es")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    [Header("Detec��o de ch�o")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se est� no ch�o com Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Mant�m o personagem no ch�o
        }

        // Movimento autom�tico no eixo X
        Vector3 move = Vector3.right;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Pulo
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }

        // Aplica gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}