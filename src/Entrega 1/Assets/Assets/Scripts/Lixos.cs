using UnityEngine;

public class Lixos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é o jogador
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        // Se for, chama o método para coletar lixo e desativa o objeto de lixo
        if (playerInventory != null)
        {
            // Chama o método para coletar lixo no inventário do jogador
            playerInventory.lixosColetados();
            gameObject.SetActive(false);
        }
    }
}
