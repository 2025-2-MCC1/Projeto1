using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

    public Player playerScript;
    public GameObject gameOverMenuUI;
    public GameObject levelCompleteMenuUI;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
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
        if (gameOverMenuUI != null)
        {
            gameOverMenuUI.SetActive(true);
        }
    }

    public void ShowLevelCompleteMenu()
    {
        if (levelCompleteMenuUI != null)
        {
            levelCompleteMenuUI.SetActive(true);
        }
    }
}
