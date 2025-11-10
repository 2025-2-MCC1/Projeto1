using UnityEngine;

public class movimentação : MonoBehaviour
{
    private bool canMove = true; 

    void Update()
    {
        if (!canMove) return;


        if (Input.GetKeyDown(KeyCode.W) && (transform.position.z < 3.5f))
        {
            transform.position += new Vector3(0, 0, 1f);
        }

        if (Input.GetKeyDown(KeyCode.S) && (transform.position.z > -3.5f))
        {
            transform.position += new Vector3(0, 0, -1f);
        }

        if (Input.GetKeyDown(KeyCode.A) && (transform.position.x > -3.5f))
        {
            transform.position += new Vector3(-1f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && (transform.position.x < 3.5f))
        {
            transform.position += new Vector3(1f, 0, 0);
        }
    }
}