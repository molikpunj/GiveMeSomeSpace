using UnityEngine;

public class EnergyRayBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 15 * Time.deltaTime);

        if(transform.position.y > 0 || transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
