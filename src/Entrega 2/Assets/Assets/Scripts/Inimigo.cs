using UnityEngine;

public class Inimigo : MonoBehaviour
{
    // Referências para a posição inicial do inimigo
    public Vector3 initialPosition;

    // Velocidades de movimento
    public float velocidadeHorizontal = 0f;
    public float velocidadeVertical = 0f;

    // Limites de movimento
    public float limiteDireita = 2.5f;
    public float limiteEsquerda = -1.5f;

    public float limiteFrente = 2.5f;
    public float limiteTras = -1.5f;

    // Direção atual do movimento
    private float direcaoHorizontal = 1f;
    private float direcaoVertical = 1f;

    void Start()
    {
        // Reseta a posição inicial do inimigo
        initialPosition = transform.position;
    }

    void Update()
    {
        // Movimento horizontal
        if (velocidadeHorizontal != 0f)
        {
            // Move no eixo X
            transform.Translate(Vector3.right * direcaoHorizontal * velocidadeHorizontal * Time.deltaTime);

            // Inverte direção ao atingir limites
            if (transform.position.x >= limiteDireita) direcaoHorizontal = -1f;
            if (transform.position.x <= limiteEsquerda) direcaoHorizontal = 1f;
        }

        // Movimento vertical
        else if (velocidadeVertical != 0f)
        {
            // Move no eixo Z
            transform.Translate(Vector3.forward * direcaoVertical * velocidadeVertical * Time.deltaTime);

            // Inverte direção ao atingir limites
            if (transform.position.z >= limiteFrente) direcaoVertical = -1f;
            if (transform.position.z <= limiteTras) direcaoVertical = 1f;
        }
    }
}

