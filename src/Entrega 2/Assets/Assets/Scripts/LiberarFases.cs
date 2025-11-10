using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LiberarFases : MonoBehaviour
{
    // Referências aos botões das fases
    public Button botaoFase2;
    public Button botaoFase3;

    public int quantidadeDeFases = 3;

    void Start()
    {
        // Calcula o total de estrelas coletadas em todas as fases
        int totalStars = 0;
        for (int i = 1; i <= quantidadeDeFases; i++)
            totalStars += PlayerPrefs.GetInt("Estrelas_Fase" + i, 0);

        // Libera as fases com base no total de estrelas coletadas
        botaoFase2.interactable = totalStars >= 2;
        botaoFase3.interactable = totalStars >= 5;
    }
}
