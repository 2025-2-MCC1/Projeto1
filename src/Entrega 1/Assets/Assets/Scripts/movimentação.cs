using UnityEngine;

// ===== SCRIPT DE MOVIMENTAÇÃO WASD COM PULO E FÍSICA =====
// Este script controla o movimento de um personagem usando WASD e pulo com Espaço
// Usa Rigidbody para física realista (gravidade, colisões, etc.)
public class movimentação : MonoBehaviour
{
    // ===== CONFIGURAÇÕES PÚBLICAS (aparecem no Inspector) =====
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade de movimento horizontal (WASD)")]
    public float velocidadeMovimento = 5f; // Define quão rápido o personagem anda
    
    [Tooltip("Força do pulo (quanto maior, mais alto pula)")]
    public float forcaPulo = 5f; // Define a força aplicada ao pular
    
    [Header("Detecção de Chão")]
    [Tooltip("Distância do raycast para detectar o chão")]
    public float distanciaRaycast = 1.1f; // Distância para detectar se está no chão
    
    // ===== VARIÁVEIS PRIVADAS (internas do script) =====
    private Rigidbody rb; // Referência ao componente Rigidbody (física)
    private bool estaNoChao; // Verifica se o personagem está tocando o chão
    private Vector3 direcaoMovimento; // Armazena a direção do movimento calculada
    
    // ===== FUNÇÃO START - Executada uma vez ao iniciar =====
    void Start()
    {
        // Busca o componente Rigidbody anexado ao personagem
        rb = GetComponent<Rigidbody>();
        
        // Verifica se o Rigidbody existe, senão mostra erro
        if (rb == null)
        {
            Debug.LogError("ERRO: Rigidbody não encontrado! Adicione um Rigidbody ao personagem.");
        }
        
        // Configura o Rigidbody para não rotacionar com colisões (evita tombar)
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        
        // Sistema de detecção de chão simplificado (não precisa configurar nada)
    }
    
    // ===== FUNÇÃO UPDATE - Executada a cada frame =====
    void Update()
    {
        // Chama a função que detecta se está no chão
        VerificarSeEstaNoChao();
        
        // Chama a função que captura as teclas WASD
        CapturarInput();
        
        // Chama a função que processa o pulo
        ProcessarPulo();
    }
    
    // ===== FUNÇÃO FIXEDUPDATE - Executada em intervalos fixos (melhor para física) =====
    void FixedUpdate()
    {
        // Aplica o movimento calculado usando física
        AplicarMovimento();
    }
    
    // ===== VERIFICA SE O PERSONAGEM ESTÁ TOCANDO O CHÃO =====
    void VerificarSeEstaNoChao()
    {
        // Lança um raio para baixo a partir do centro do personagem
        // Se o raio atingir algo, está no chão
        estaNoChao = Physics.Raycast(transform.position, Vector3.down, distanciaRaycast);
    }
    
    // ===== CAPTURA AS TECLAS WASD DO TECLADO =====
    void CapturarInput()
    {
        // Input.GetAxis retorna valores entre -1 e 1
        // "Horizontal" = A/D ou Setas Esquerda/Direita (-1 = esquerda, 1 = direita)
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        // "Vertical" = W/S ou Setas Cima/Baixo (-1 = trás, 1 = frente)
        float inputVertical = Input.GetAxis("Vertical");
        
        // Cria um vetor de direção baseado no input
        // X = movimento lateral (esquerda/direita)
        // Y = 0 (não mexe na altura aqui, só com pulo)
        // Z = movimento frontal (frente/trás)
        direcaoMovimento = new Vector3(inputHorizontal, 0f, inputVertical);
        
        // Normaliza o vetor para evitar movimento mais rápido na diagonal
        // (se apertar W+D ao mesmo tempo, a velocidade seria maior sem normalizar)
        direcaoMovimento = direcaoMovimento.normalized;
    }
    
    // ===== PROCESSA O PULO (TECLA ESPAÇO) =====
    void ProcessarPulo()
    {
        // Verifica se apertou a tecla Espaço E está no chão
        // (só pode pular se estiver tocando o chão)
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            // Aplica uma força para cima (Vector3.up = direção Y positiva)
            // ForceMode.Impulse = aplica força instantânea (boa para pulos)
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            
            // Log opcional para debug
            Debug.Log("Pulou!");
        }
    }
    
    // ===== APLICA O MOVIMENTO USANDO FÍSICA =====
    void AplicarMovimento()
    {
        // Se não há Rigidbody, não pode aplicar movimento
        if (rb == null) return;
        
        // Calcula a velocidade desejada multiplicando direção pela velocidade
        Vector3 velocidadeDesejada = direcaoMovimento * velocidadeMovimento;
        
        // Cria um vetor de velocidade mantendo a velocidade vertical atual (Y)
        // Isso preserva a gravidade e o pulo, modificando apenas X e Z
        Vector3 novaVelocidade = new Vector3(
            velocidadeDesejada.x,  // Nova velocidade horizontal X
            rb.linearVelocity.y,          // Mantém velocidade vertical (gravidade/pulo)
            velocidadeDesejada.z   // Nova velocidade horizontal Z
        );
        
        // Aplica a nova velocidade ao Rigidbody
        rb.linearVelocity = novaVelocidade;
    }
    
    // ===== DESENHA GIZMOS NO EDITOR (apenas para visualização) =====
    // Esta função só funciona no Editor do Unity, não no jogo compilado
    void OnDrawGizmosSelected()
    {
        // Define a cor do Gizmo (verde se está no chão, vermelho se não está)
        Gizmos.color = estaNoChao ? Color.green : Color.red;
        
        // Desenha uma linha representando o raycast de detecção do chão
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaRaycast);
    }
}

// ===== INSTRUÇÕES DE CONFIGURAÇÃO NO UNITY =====
// 1. Adicione este script ao seu personagem
// 2. Adicione um Rigidbody ao personagem (Add Component > Physics > Rigidbody)
//    - Mass: 1
//    - Drag: 0
//    - Angular Drag: 0.05
//    - Use Gravity: ✓ (marcado)
//    - Is Kinematic: ✗ (desmarcado)
//    - Collision Detection: Continuous
//    - Constraints: Freeze Rotation X, Z (para não tombar)
//
// 3. Adicione um Collider ao personagem (Capsule Collider é melhor)
//    - Add Component > Physics > Capsule Collider
//    - Center: (0, 1, 0) - ajuste conforme seu personagem
//    - Radius: 0.5
//    - Height: 2
//
// 4. Configure o chão:
//    - Selecione o objeto que é o chão
//    - No Inspector, mude a Layer para "Default" ou crie uma layer "Ground"
//    - Adicione um Collider ao chão (Box Collider)
//
// 5. No Inspector do personagem, configure:
//    - Velocidade Movimento: 5
//    - Força Pulo: 5
//    - Distancia Raycast: 1.1 (ajuste conforme a altura do personagem)
