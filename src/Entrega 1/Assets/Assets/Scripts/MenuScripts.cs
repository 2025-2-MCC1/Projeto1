using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

    public Player playerScript;
    public GameObject gameOverMenuUI;
    public GameObject levelCompleteMenuUI;

    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene(2);
    }
    public void SelecionarFase1()
    {
        SceneManager.LoadScene(3);
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("Options.ShowGameOverMenu called on: " + gameObject.name);
        if (gameOverMenuUI != null)
        {
            Debug.Log("Activating gameOverMenuUI: " + gameOverMenuUI.name);
            gameOverMenuUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("gameOverMenuUI reference is null on " + gameObject.name);
        }
    }

    public void ShowLevelCompleteMenu()
    {
        Debug.Log("Options.ShowLevelCompleteMenu called on: " + gameObject.name);
        if (levelCompleteMenuUI != null)
        {
            Debug.Log("Activating levelCompleteMenuUI: " + levelCompleteMenuUI.name);
            levelCompleteMenuUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("levelCompleteMenuUI reference is null on " + gameObject.name);
        }
    }
    public void ResetGame()
    {
        // Garantir que o tempo do jogo esteja normalizado antes de reiniciar a cena
        Debug.Log("Options.ResetGame called - restoring Time.timeScale and reloading scene");
        Time.timeScale =1f;

        // Recarrega a cena atual para restaurar todos os objetos ao estado inicial.
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);

        // Nota: se preferir reset manual sem recarregar a cena, implemente ResetState() nos objetos relevantes.
    }
}
