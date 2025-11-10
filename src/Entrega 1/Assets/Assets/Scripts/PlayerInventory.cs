using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    // Número de lixos coletados pelo jogador
    public int NumeroLixos { get; private set;}

    // Evento para notificar quando lixos são coletados
    public UnityEvent<PlayerInventory> onLixosCollected;

    public void lixosColetados()
    {
        // Incrementa o número de lixos coletados ao colidir com um lixo
        NumeroLixos++;
        // Notifica listeners para atualizar UI
        onLixosCollected.Invoke(this);
    }

    // Reseta o inventário (usado ao reiniciar a fase)
    public void ResetInventory()
    {
        NumeroLixos = 0;
        // Notifica listeners para atualizar UI
        onLixosCollected?.Invoke(this);
    }
}
