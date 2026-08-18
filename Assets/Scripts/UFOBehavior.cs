using UnityEngine;

public class UFOBehavior : MonoBehaviour
{
    private float rotationDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationDirection = Random.Range(-50, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationDirection * Time.deltaTime);
    }
}
