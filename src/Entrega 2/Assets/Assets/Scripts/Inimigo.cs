using UnityEngine;

public class Inimigo : MonoBehaviour
{
    // Posição inicial do inimigo
    public Vector3 initialPosition;

    // Velocidade de movimento do inimigo
    public float velocidade = 5f;

    // limites do movimento no eixo X
    public float limiteDireita = 2.5f;
    public float limiteEsquerda = -1.5f;

    private float direcao = 3f;

    void Start()
    {
        // Transforma a posição inicial do inimigo ao resetar a fase
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move o inimigo na direção atual
        Vector3 deslocamento = Vector3.right * direcao * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);

        // Inverte a direção ao atingir os limites estabelecidos
        if (transform.position.x >= limiteDireita && direcao > 0f)
        {
            direcao = -3f;
        }
        else if (transform.position.x <= limiteEsquerda && direcao < 0f)
        {
            direcao = 3f;
        }
    }
}
