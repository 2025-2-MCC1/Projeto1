using UnityEngine;
using TMPro;

public class MostrarTotalEstrelas : MonoBehaviour
{
    // Referência ao componente de texto para exibir o total de estrelas
    public TMP_Text textoTotalEstrelas;

    public int quantidadeDeFases = 3;

    void Start()
    {
        // Calcula o total de estrelas coletadas em todas as fases
        int total = 0;
        for (int i = 1; i <= quantidadeDeFases; i++)
        {
            total += PlayerPrefs.GetInt("Estrelas_Fase" + i, 0);
        }

        // Atualiza o texto com o total de estrelas
        textoTotalEstrelas.text = total.ToString();
    }
}
