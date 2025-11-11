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
        // Verifica se o jogador está vivo
        if (!isAlive)
        {
            canMove = false;
            return;
        }

        if (!canMove) return;

        bool moved = false;

        // Movimento do jogador baseado na entrada do teclado
        if (Input.GetKeyDown(KeyCode.W) && (transform.position.z < 4.5f))
        {
            transform.position += new Vector3(0, 0, 1f);
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && (transform.position.z > -4.5f))
        {
            transform.position += new Vector3(0, 0, -1f);
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && (transform.position.x > -4.5f))
        {
            transform.position += new Vector3(-1f, 0, 0);
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && (transform.position.x < 4.5f))
        {
            transform.position += new Vector3(1f, 0, 0);
            moved = true;
        }

        // Atualiza o contador de passos se o jogador se moveu
        if (moved)
        {
            currentSteps++;
            Debug.Log($"Passos: {currentSteps}/{maxStepsAllowed}");
            UpdateStepsUI();

            if (maxStepsAllowed > 0 && currentSteps >= maxStepsAllowed)
            {
                canMove = false;
                isAlive = false;
                Time.timeScale = 0f;
                Debug.LogWarning("Game Over: Passos esgotados!");
                menuScript?.ShowGameOverMenu();
            }
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

        canMove = false;

        Debug.Log("Estrelas obtidas: " + estrelas);

        menuScript?.ShowLevelCompleteMenu();
        if (levelComplete != null) levelComplete.SetActive(true);
        fimDeFaseUI?.MostrarEstrelas(estrelas);
        SalvarEstrelas(estrelas);
    }

    // Salva o número de estrelas obtidas na fase
    public void SalvarEstrelas(int estrelas)
    {
        string chave = "Estrelas_Fase" + numeroDaFase;

        int estrelasSalvas = PlayerPrefs.GetInt(chave, 0);

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
