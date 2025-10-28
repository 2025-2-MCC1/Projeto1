using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Target;
    public float rotationSpeed =90f;
    public float yOffset =6f;
    public float zOffset =6f;

    private float initialYOffset;

    void Start()
    {
        if (Target == null) return;
        // Guarda a diferença de altura inicial entre câmera e target para mantê-la constante
        initialYOffset = transform.position.y - Target.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        float delta =0f;
        if (Input.GetKey(KeyCode.Q))
        {
            delta = rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            delta = -rotationSpeed * Time.deltaTime;
        }

        if (delta !=0f)
        {
            Orbit(Target.position, Vector3.up, delta);
        }
    }

    void Orbit(Vector3 point, Vector3 axis, float angle)
    {
        // Rotaciona em torno do ponto no eixo Y
        transform.RotateAround(point, axis, angle);

        // Mantém a altura constante baseada na altura inicial da câmera relativa ao target
        Vector3 newPosition = transform.position;
        newPosition.y = Target.position.y + initialYOffset;
        transform.position = newPosition;

        // Olha para o target
        transform.LookAt(Target);
    }
}
