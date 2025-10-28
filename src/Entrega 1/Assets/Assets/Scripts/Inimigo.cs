using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Vector3 initialPosition;

    [Tooltip("Velocidade de movimento em unidades por segundo")]
    public float velocidade = 1f;

    // limites do movimento no eixo X
    public float limiteDireita = 2.5f;
    public float limiteEsquerda = -1.5f;

    // direção atual:1 = direita, -1 = esquerda
    private float direcao = 1f;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move o inimigo na direção atual
        Vector3 deslocamento = Vector3.right * direcao * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);

        // Inverte a direção ao atingir os limites
        if (transform.position.x >= limiteDireita && direcao > 0f)
        {
            direcao = -1f;
        }
        else if (transform.position.x <= limiteEsquerda && direcao < 0f)
        {
            direcao = 1f;
        }
    }
}
