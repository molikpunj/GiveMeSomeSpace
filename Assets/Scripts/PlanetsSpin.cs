using UnityEngine;

public class PlanetsSpin : MonoBehaviour
{
    public GameManager GameManager;
    private int planetHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        float scale = Random.Range(50, 100);
        transform.localScale = new Vector3(scale, scale, scale);
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        planetHealth = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20 || transform.position.y > 0)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0, 30 * Time.deltaTime, 30 * Time.deltaTime);
        if (!GameManager.gameOver)
        {
            transform.position += Vector3.down * GameManager.planetSpeed * Time.deltaTime;
        }
        if (planetHealth <= 0)
        {
            GameManager.destroyedCount++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyRay"))
        {
            planetHealth--;
            Destroy(other.gameObject);
        }
    }
}
