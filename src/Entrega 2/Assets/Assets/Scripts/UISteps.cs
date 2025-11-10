using UnityEngine;
using TMPro;

public class UISteps : MonoBehaviour
{
    // Recebe o componente de texto da UI para exibir o número de lixos coletados
    public TextMeshProUGUI stepsText;

    void Start()
    {
        // Inicializa o componente de texto dos passos na UI
        stepsText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStepsText(int currentSteps, int maxSteps)
    {
        // Atualiza o texto para mostrar o número atual de passos restantes
        if (maxSteps > 0)
        {
            int stepsRemaining = maxSteps - currentSteps;
            stepsText.text = $"Passos: {stepsRemaining}/{maxSteps}";
        }
        else
        {
            stepsText.text = "Passos: ∞";
        }
    }
}
