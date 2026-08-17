using UnityEngine;

public class PlanetsSpin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        float scale = Random.Range(35, 100);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20 || transform.position.y > 0)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0, 30 * Time.deltaTime, 30 * Time.deltaTime);
        transform.position += Vector3.down * Time.deltaTime;
    }
}
