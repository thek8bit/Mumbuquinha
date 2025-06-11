using UnityEngine;

public class ShadowProjector : MonoBehaviour
{
    public Transform target;         // O personagem
    public float offsetY = 0.05f;    // Pequena eleva��o para evitar flickering
    public float maxDistance = 20f;  // M�xima dist�ncia do raycast
    public GameObject shadow;        // A imagem da sombra (ex: um quad com textura)

    void Update()
    {
        if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, maxDistance))
        {
            shadow.transform.position = hit.point + Vector3.up * offsetY;

            // Alinha a sombra � superf�cie
            shadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if (!shadow.activeSelf)
                shadow.SetActive(true);
        }
        else
        {
            // Se n�o atingir nada, desativa a sombra
            if (shadow.activeSelf)
                shadow.SetActive(false);
        }
    }
}
