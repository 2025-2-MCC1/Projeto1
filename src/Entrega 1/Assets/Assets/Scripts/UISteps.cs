using UnityEngine;
using TMPro;

public class UISteps : MonoBehaviour
{
    public TextMeshProUGUI stepsText;

    void Start()
    {
        stepsText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStepsText(int currentSteps, int maxSteps)
    {
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
