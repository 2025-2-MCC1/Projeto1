using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class UILixos : MonoBehaviour
{

    public TextMeshProUGUI lixosText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lixosText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateLixosText(PlayerInventory playerInventory)
    {
        lixosText.text = playerInventory.NumeroLixos.ToString();
    }
}


