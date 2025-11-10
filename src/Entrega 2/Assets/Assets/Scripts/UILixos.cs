using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILixos : MonoBehaviour
{
    // Recebe o componente de texto da UI para exibir o número de lixos coletados
    public TextMeshProUGUI lixosText;

    void Start()
    {
        // Inicializa o componente de texto
        lixosText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLixosText(PlayerInventory playerInventory)
    {
        // Atualiza o texto para mostrar o número atual de lixos coletados
        lixosText.text = playerInventory.NumeroLixos.ToString();
    }
}


