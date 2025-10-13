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
}
