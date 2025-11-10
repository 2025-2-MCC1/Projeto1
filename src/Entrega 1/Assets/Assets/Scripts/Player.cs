using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Options menuScript; // Referï¿½ncia ao script de menu (classe `Options` em MenuScripts.cs)
    [SerializeField] private UISteps uiSteps; // Referência ao script de UI de passos

    private PlayerInventory playerInventory;

    private int health = 1;
    private bool canMove = true;
    private bool isAlive = true;

    private Vector3 initialPosition;

    [Header("Level Settings")]
    [Tooltip("Quantidade total de lixos nesta fase. Ajuste conforme a fase.")]
    [SerializeField] private int totalLixosInLevel = 3;

    [Header("Step Limit Settings")]
    [Tooltip("Limite de passos permitidos nesta fase. 0 = sem limite.")]
    [SerializeField] private int maxStepsAllowed = 20;

    private int currentSteps = 0;

    void Awake()
    {
        initialPosition = transform.position;
        playerInventory = GetComponent<PlayerInventory>();
        ResetPlayerState();
        UpdateStepsUI();
    }

    void Update()
    {
        if (!isAlive)
        {
            canMove = false;
            return;
        }

        if (!canMove) return;

        bool moved = false;

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

        // Incrementa contador se houve movimento
        if (moved)
        {
            currentSteps++;
            Debug.Log($"Passos: {currentSteps}/{maxStepsAllowed}");
            UpdateStepsUI();
            
            // Verifica se os passos acabaram após o movimento
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

    void OnTriggerEnter(Collider bater)
    {
        HandleHit(bater.gameObject);
    }

    private void HandleHit(GameObject bater)
    {
        // Verifica se o objeto atingido tem o componente Inimigo
        if (bater.GetComponent<Inimigo>() != null)
        {
            DamagePlayer();
            return;
        }

        // Verifica se chegou na Flag. Evita chamar CompareTag se a tag nï¿½o estiver definida.
        bool isFlag = false;

        if (bater.name == "Flag")
        {
            isFlag = true;
        }
        else
        {
            // CompareTag lanï¿½a UnityException se a tag nï¿½o existe. Capturamos para evitar log repetido.
            try
            {
                if (bater.CompareTag("Flag"))
                    isFlag = true;
            }
            catch (UnityException)
            {
                // Tag nï¿½o definida ï¿½ ignora a comparaï¿½ï¿½o para evitar mensagens no console.
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
        int lixosColetados = playerInventory != null ? playerInventory.NumeroLixos : 0;
        int lixosFaltando = totalLixosInLevel - lixosColetados;

        int estrelas;
        string mensagem;

        if (lixosFaltando <= 0)
        {
            estrelas = 3;
            mensagem = "Chegou a bandeira e coletou todos os lixos: 3 estrelas";
        }
        else if (lixosFaltando == 1)
        {
            estrelas = 2;
            mensagem = "Chegou a bandeira mas faltou um lixo: 2 estrelas";
        }
        else if (lixosFaltando == 2)
        {
            estrelas = 1;
            mensagem = "Chegou a bandeira mas faltaram 2 lixos: 1 estrela";
        }
        else
        {
            estrelas = 0;
            mensagem = "Chegou a bandeira com um lixo ou menos: 0 estrelas";
        }

        // Desabilita movimento ao concluir
        canMove = false;

        // Log no console por enquanto
        Debug.Log(mensagem + $" (collected={lixosColetados}, total={totalLixosInLevel}, steps={currentSteps}/{maxStepsAllowed})");
        Debug.Log("Estrelas obtidas: " + estrelas);

        menuScript?.ShowLevelCompleteMenu();
    }

    public void DamagePlayer()
    {
        health--;
        if (health <= 0)
        {
            isAlive = false;
            canMove = false;
            Time.timeScale = 0f;
        }

        menuScript?.ShowGameOverMenu();
    }

    public void ResetPlayerState()
    {
        Debug.Log("resetou");
        health = 1;
        isAlive = true;
        canMove = true;
        Time.timeScale = 1f;
        currentSteps = 0; // Reseta o contador de passos

        transform.position = initialPosition;
        UpdateStepsUI();
    }

    private void UpdateStepsUI()
    {
        if (uiSteps != null)
        {
            uiSteps.UpdateStepsText(currentSteps, maxStepsAllowed);
        }
    }
}