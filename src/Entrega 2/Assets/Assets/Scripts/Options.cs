using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Referência ao script do jogador
    public Player playerScript;

    // Referências aos menus de UI
    public GameObject gameOverMenuUI;
    public GameObject levelCompleteMenuUI;
    public bool isMenu = false;

    // Referências às estrelas na UI de conclusão de fase
    public Image star1;
    public Image star2;
    public Image star3;

    // Método para mostrar estrelas com base na pontuação
    public void MostrarEstrelas(int estrelasAtuais)
    {
        Debug.Log("Mostrando estrelas no objeto: " + gameObject.name);

        star1.enabled = estrelasAtuais >= 1;
        star2.enabled = estrelasAtuais >= 2;
        star3.enabled = estrelasAtuais >= 3;
    }

    void Start()
    {
        if (isMenu)
        {
            int estrelasSalvas = PlayerPrefs.GetInt("Estrelas_Fase" + playerScript.numeroDaFase, 0);
            MostrarEstrelas(estrelasSalvas);
        }
    }



    // Método para mostrar menu de GameOver
    public void ShowGameOverMenu()
    {
        if (gameOverMenuUI != null)
        {
            gameOverMenuUI.SetActive(true);
        }
    }

    // Método para mostrar menu de conclusão de fase
    public void ShowLevelCompleteMenu()
    {
        if (levelCompleteMenuUI != null)
        {
            levelCompleteMenuUI.SetActive(true);
        }
    }
}