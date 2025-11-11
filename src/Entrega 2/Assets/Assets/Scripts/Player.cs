using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Referências aos scripts e objetos de UI
    [SerializeField] private Options menuScript;
    [SerializeField] private UISteps uiSteps;
    [SerializeField] private Options fimDeFaseUI;
    [SerializeField] private GameObject levelComplete;

    // Referência ao inventário do jogador
    private PlayerInventory playerInventory;

    // Número da fase atual
    public int numeroDaFase = 1;

    // Estado do jogador
    private int health = 1;
    private bool canMove = true;
    private bool isAlive = true;

    // Posição inicial do jogador
    private Vector3 initialPosition;

    // Configurações da fase
    [Header("Level Settings")]
    [Tooltip("Quantidade total de lixos nesta fase. Ajuste conforme a fase.")]
    [SerializeField] private int totalLixosInLevel = 3;

    [Header("Step Limit Settings")]
    [Tooltip("Limite de passos permitidos nesta fase. 0 = sem limite.")]
    [SerializeField] private int maxStepsAllowed = 20;

    // Contador de passos atuais
    private int currentSteps = 0;

    // Inicialização do jogador na fase
    void Awake()
    {
        initialPosition = transform.position;
        playerInventory = GetComponent<PlayerInventory>();
        ResetPlayerState();
        UpdateStepsUI();
    }

    void Update()
    {
        if (!isAlive || !canMove) return;

        Vector3 direcao = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) && transform.position.z < 4.5f)
            direcao = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -4.5f)
            direcao = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -4.5f)
            direcao = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 4.5f)
            direcao = Vector3.right;

        // se nenhuma tecla foi pressionada, sai
        if (direcao == Vector3.zero) return;

        // verifica obstáculo
        if (!Physics.Raycast(transform.position, direcao, 1f, LayerMask.GetMask("Wall")))
        {
            transform.position += direcao; // ✅ move
            currentSteps++; // ✅ conta passo somente agora
            Debug.Log($"Passos: {currentSteps}/{maxStepsAllowed}");
            UpdateStepsUI();
        }
        else
        {
            Debug.Log("Movimento bloqueado por parede.");
        }

        // limite de passos
        if (maxStepsAllowed > 0 && currentSteps >= maxStepsAllowed)
        {
            canMove = false;
            isAlive = false;
            Time.timeScale = 0f;
            Debug.LogWarning("Game Over: Passos esgotados!");
            menuScript?.ShowGameOverMenu();
        }
    }

    // Detecta colisões com outros objetos
    void OnTriggerEnter(Collider bater)
    {
        HandleHit(bater.gameObject);
    }

    private void HandleHit(GameObject bater)
    {
        // Verifica se colidiu com um inimigo
        if (bater.GetComponent<Inimigo>() != null)
        {
            DamagePlayer();
            return;
        }

        // Verifica se colidiu com a bandeira
        bool isFlag = false;

        if (bater.name == "Flag")
        {
            isFlag = true;
        }
        else
        {
            try
            {
                if (bater.CompareTag("Flag"))
                    isFlag = true;
            }
            catch (UnityException)
            {
            }
        }

        if (isFlag)
        {
            OnReachedFlag();
            return;
        }
    }

    private void OnReachedFlag()
    {
        // Calcula o número de lixos coletados e determina a quantidade de estrelas obtidas na fase
        int lixosColetados = playerInventory != null ? playerInventory.NumeroLixos : 0;
        int lixosFaltando = totalLixosInLevel - lixosColetados;

        int estrelas;
        if (lixosFaltando <= 0)
        {
            estrelas = 3;
        }
        else if (lixosFaltando == 1)
        {
            estrelas = 2;
        }
        else if (lixosFaltando == 2)
        {
            estrelas = 1;
        }
        else
        {
            estrelas = 0;
        }

        // Finaliza a fase
        canMove = false;

        Debug.Log("Estrelas obtidas: " + estrelas);

        // Exibe o menu de conclusão de fase
        menuScript?.ShowLevelCompleteMenu();

        // Ativa o painel de fim de fase e mostra as estrelas obtidas
        if (levelComplete != null) levelComplete.SetActive(true);

        // Mostra as estrelas na UI de fim de fase
        fimDeFaseUI?.MostrarEstrelas(estrelas);

        // Chama a função que salva o número de estrelas obtidas
        SalvarEstrelas(estrelas);
    }

    // Salva o número de estrelas obtidas na fase
    public void SalvarEstrelas(int estrelas)
    {
        // Cria uma chave única para a fase atual
        string chave = "Estrelas_Fase" + numeroDaFase;

        // Recupera o número de estrelas já salvas para esta fase
        int estrelasSalvas = PlayerPrefs.GetInt(chave, 0);

        // Salva o novo número de estrelas somente se for maior que o salvo anteriormente
        if (estrelas > estrelasSalvas)
        {
            PlayerPrefs.SetInt(chave, estrelas);
            PlayerPrefs.Save();
        }
    }


    // Aplica dano ao jogador ao colidir com um inimigo
    public void DamagePlayer()
    {
        health--;
        if (health <= 0)
        {
            isAlive = false;
            canMove = false;
            Time.timeScale = 0f;
        }

        // Exibe o menu de Game Over se o jogador morrer
        menuScript?.ShowGameOverMenu();
    }

    // Reseta o estado do jogador para o do início da fase
    public void ResetPlayerState()
    {
        Debug.Log("resetou");
        health = 1;
        isAlive = true;
        canMove = true;
        Time.timeScale = 1f;
        currentSteps = 0;

        transform.position = initialPosition;
        UpdateStepsUI();
    }

    // Atualiza a UI de passos
    private void UpdateStepsUI()
    {
        if (uiSteps != null)
        {
            uiSteps.UpdateStepsText(currentSteps, maxStepsAllowed);
        }
    }

}
