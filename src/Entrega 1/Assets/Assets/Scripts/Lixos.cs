using UnityEngine;

public class Lixos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.lixosColetados();
            gameObject.SetActive(false);
        }
    }
}
