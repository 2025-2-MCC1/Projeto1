using Unity.VisualScripting;
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
    public void SelecionarFase2()
    {
        SceneManager.LoadScene(3);
    }
    public void SelecionarFase3()
    {
        SceneManager.LoadScene(4);
    }

    // Método para resetar o progresso do jogador (Apenas testes, não terá na versão final)
    public void ResetarProgresso()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progresso resetado!");
    }
}
