using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Target;
    public float rotationSpeed = 90f;
    public float  yOffset = 6f;
    public float zOffset = 6f;

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        float angle = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            Orbit(Target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.E))
        {
            Orbit(Target.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (angle != 0)
        {
            Orbit(Target.position, Vector3.up, angle);
        }
    }

    void Orbit(Vector3 point, Vector3 axis, float angle)
    {
        transform.RotateAround(point, axis, angle);

        // Mantém a altura constante
        Vector3 newPosition = transform.position;
        newPosition.y = Target.position.y + yOffset;
        transform.position = newPosition;

        transform.LookAt(Target);

    }
}
