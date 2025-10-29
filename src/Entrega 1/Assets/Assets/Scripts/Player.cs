using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Options menuScript; // Referência ao script de menu (classe `Options` em MenuScripts.cs)

    private PlayerInventory playerInventory;

    private int health = 1;
    private bool canMove = true;
    private bool isAlive = true;

    private Vector3 initialPosition;

    [Header("Level Settings")]
    [Tooltip("Quantidade total de lixos nesta fase. Ajuste conforme a fase.")]
    [SerializeField] private int totalLixosInLevel = 3;

    void Awake()
    {
        initialPosition = transform.position;
        playerInventory = GetComponent<PlayerInventory>();
        ResetPlayerState();
    }

    void Update()
    {
        if (!isAlive)
        {
            canMove = false;
            return;
        }

        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.W) && (transform.position.z < 4.5f))
        {
            transform.position += new Vector3(0, 0, 1f);
        }

        if (Input.GetKeyDown(KeyCode.S) && (transform.position.z > -4.5f))
        {
            transform.position += new Vector3(0, 0, -1f);
        }

        if (Input.GetKeyDown(KeyCode.A) && (transform.position.x > -4.5f))
        {
            transform.position += new Vector3(-1f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && (transform.position.x < 4.5f))
        {
            transform.position += new Vector3(1f, 0, 0);
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

        // Verifica se chegou na Flag. Evita chamar CompareTag se a tag não estiver definida.
        bool isFlag = false;

        if (bater.name == "Flag")
        {
            isFlag = true;
        }
        else
        {
            // CompareTag lança UnityException se a tag não existe. Capturamos para evitar log repetido.
            try
            {
                if (bater.CompareTag("Flag"))
                    isFlag = true;
            }
            catch (UnityException)
            {
                // Tag não definida — ignora a comparação para evitar mensagens no console.
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
        Debug.Log(mensagem + $" (collected={lixosColetados}, total={totalLixosInLevel})");
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

        transform.position = initialPosition;
    }
}