using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    // Métodos para navegação entre cenas
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void SelecionarFase1()
    {
        SceneManager.LoadScene(2);
    }
}
