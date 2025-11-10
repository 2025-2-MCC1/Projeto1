using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumeroLixos { get; private set;}

    public UnityEvent<PlayerInventory> onLixosCollected;

    public void lixosColetados()
    {
        NumeroLixos++;
        onLixosCollected.Invoke(this);
    }

    // Reseta o inventário (usado ao reiniciar a fase)
    public void ResetInventory()
    {
        NumeroLixos =0;
        // Notifica listeners para atualizar UI
        onLixosCollected?.Invoke(this);
    }
}
