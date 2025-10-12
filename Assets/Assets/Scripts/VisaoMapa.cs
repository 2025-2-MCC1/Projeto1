using UnityEngine;

public class VisaoMapa : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade de movimento lateral da câmera")]
    public float velocidadeMovimento = 10f;
    
    [Tooltip("Usar movimento suave (smooth)?")]
    public bool movimentoSuave = true;
    
    [Tooltip("Velocidade de suavização (maior = mais rápido)")]
    public float velocidadeSuavizacao = 5f;
    
    [Header("Controles")]
    [Tooltip("Tecla para mover para a esquerda")]
    public KeyCode teclaEsquerda = KeyCode.A;
    
    [Tooltip("Tecla para mover para a direita")]
    public KeyCode teclaDireita = KeyCode.D;
    
    [Tooltip("Tecla para mover para cima (opcional)")]
    public KeyCode teclaCima = KeyCode.W;
    
    [Tooltip("Tecla para mover para baixo (opcional)")]
    public KeyCode teclaBaixo = KeyCode.S;
    
    [Tooltip("Tecla para resetar a câmera")]
    public KeyCode teclaResetar = KeyCode.R;
    
    [Tooltip("Habilitar movimento vertical (W/S)?")]
    public bool habilitarMovimentoVertical = false;
    
    [Header("Limites do Mapa")]
    [Tooltip("Usar limites para não sair do mapa?")]
    public bool usarLimites = true;
    
    [Tooltip("Limite mínimo no eixo X (esquerda)")]
    public float limiteMinX = -60f;
    
    [Tooltip("Limite máximo no eixo X (direita)")]
    public float limiteMaxX = 0f;
    
    [Tooltip("Limite mínimo no eixo Z (trás)")]
    public float limiteMinZ = 30f;
    
    [Tooltip("Limite máximo no eixo Z (frente)")]
    public float limiteMaxZ = 70f;
    
    [Header("Zoom (Opcional)")]
    [Tooltip("Habilitar zoom com scroll do mouse?")]
    public bool habilitarZoom = false;
    
    [Tooltip("Velocidade do zoom")]
    public float velocidadeZoom = 5f;
    
    [Tooltip("Distância mínima de zoom")]
    public float zoomMinimo = 5f;
    
    [Tooltip("Distância máxima de zoom")]
    public float zoomMaximo = 30f;
    
    [Header("Posição Inicial")]
    [Tooltip("Usar posição fixa ao iniciar o jogo?")]
    public bool usarPosicaoFixa = true;
    
    [Tooltip("Posição inicial da câmera (visão de cima do mapa)")]
    public Vector3 posicaoInicialFixa = new Vector3(-32.75f, 6.13f, 50.03f);
    
    // Variáveis internas
    private Vector3 posicaoAlvo;
    private Vector3 posicaoInicial;
    
    void Start()
    {
        // Se usar posição fixa, define ela no início
        if (usarPosicaoFixa)
        {
            transform.position = posicaoInicialFixa;
            posicaoInicial = posicaoInicialFixa;
            posicaoAlvo = posicaoInicialFixa;
        }
        else
        {
            // Caso contrário, usa a posição atual da câmera no editor
            posicaoInicial = transform.position;
            posicaoAlvo = transform.position;
        }
    }

    void Update()
    {
        MovimentarCamera();
        
        if (habilitarZoom)
        {
            AplicarZoom();
        }
        
        // Resetar câmera com a tecla R
        if (Input.GetKeyDown(teclaResetar))
        {
            ResetarPosicao();
        }
    }
    
    /// <summary>
    /// Controla o movimento da câmera com as teclas
    /// </summary>
    void MovimentarCamera()
    {
        Vector3 direcao = Vector3.zero;
        
        // Movimento horizontal (A/D - esquerda/direita)
        if (Input.GetKey(teclaEsquerda))
        {
            direcao += Vector3.left;
        }
        if (Input.GetKey(teclaDireita))
        {
            direcao += Vector3.right;
        }
        
        // Movimento vertical (W/S - frente/trás) se habilitado
        if (habilitarMovimentoVertical)
        {
            if (Input.GetKey(teclaCima))
            {
                direcao += Vector3.forward;
            }
            if (Input.GetKey(teclaBaixo))
            {
                direcao += Vector3.back;
            }
        }
        
        // Normaliza a direção para movimento diagonal não ser mais rápido
        if (direcao.magnitude > 0)
        {
            direcao.Normalize();
        }
        
        // Calcula a nova posição alvo
        posicaoAlvo += direcao * velocidadeMovimento * Time.deltaTime;
        
        // Aplica limites se habilitado
        if (usarLimites)
        {
            posicaoAlvo.x = Mathf.Clamp(posicaoAlvo.x, limiteMinX, limiteMaxX);
            posicaoAlvo.z = Mathf.Clamp(posicaoAlvo.z, limiteMinZ, limiteMaxZ);
        }
        
        // Move a câmera (suave ou direto)
        if (movimentoSuave)
        {
            transform.position = Vector3.Lerp(
                transform.position, 
                posicaoAlvo, 
                Time.deltaTime * velocidadeSuavizacao
            );
        }
        else
        {
            transform.position = posicaoAlvo;
        }
    }
    
    /// <summary>
    /// Aplica zoom com o scroll do mouse
    /// </summary>
    void AplicarZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll != 0)
        {
            // Calcula nova posição de zoom
            Vector3 novaPosicao = transform.position;
            novaPosicao.y -= scroll * velocidadeZoom;
            novaPosicao.y = Mathf.Clamp(novaPosicao.y, zoomMinimo, zoomMaximo);
            
            transform.position = novaPosicao;
            posicaoAlvo = transform.position;
        }
    }
    
    /// <summary>
    /// Reseta a câmera para a posição inicial
    /// </summary>
    public void ResetarPosicao()
    {
        posicaoAlvo = posicaoInicial;
        if (!movimentoSuave)
        {
            transform.position = posicaoInicial;
        }
        Debug.Log("Câmera resetada para posição inicial: " + posicaoInicial);
    }
    
    /// <summary>
    /// Move a câmera instantaneamente para uma posição específica
    /// </summary>
    public void IrParaPosicao(Vector3 novaPosicao)
    {
        posicaoAlvo = novaPosicao;
        if (!movimentoSuave)
        {
            transform.position = novaPosicao;
        }
    }
    
    /// <summary>
    /// Desenha os limites do mapa no editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (usarLimites)
        {
            Gizmos.color = Color.yellow;
            
            // Desenha um retângulo representando os limites
            Vector3 centro = new Vector3(
                (limiteMinX + limiteMaxX) / 2,
                transform.position.y,
                (limiteMinZ + limiteMaxZ) / 2
            );
            
            Vector3 tamanho = new Vector3(
                limiteMaxX - limiteMinX,
                0.1f,
                limiteMaxZ - limiteMinZ
            );
            
            Gizmos.DrawWireCube(centro, tamanho);
            
            // Desenha linhas para cada limite
            Gizmos.color = Color.red;
            
            // Limites X
            Vector3 p1 = new Vector3(limiteMinX, transform.position.y, limiteMinZ);
            Vector3 p2 = new Vector3(limiteMinX, transform.position.y, limiteMaxZ);
            Vector3 p3 = new Vector3(limiteMaxX, transform.position.y, limiteMaxZ);
            Vector3 p4 = new Vector3(limiteMaxX, transform.position.y, limiteMinZ);
            
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
    }
}
