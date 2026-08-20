using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private Vector3 movement;

    public void SetMovement(Vector3 movementDirection)
    {
        movement = movementDirection;
    }

    void Update()
    {
        transform.position += movement * Time.deltaTime;
    }
}