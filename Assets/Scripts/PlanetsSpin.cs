using System.Collections;
using UnityEngine;

public class PlanetsSpin : MonoBehaviour
{
    public GameManager GameManager;
    private int planetHealth;
    [SerializeField] private ParticleSystem bulletEffect;

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
        if(transform.position.y < -20 || transform.position.y > 5)
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
            GameManager.aliensKilled += Random.Range(1000, 30000);
            GameManager.score.text = $"{GameManager.aliensKilled}";
            GameObject explosionEffect = Instantiate(GameManager.explosion, transform.position, Quaternion.identity);
            explosionEffect.GetComponent<ExplosionEffect>().SetMovement(Vector3.down * GameManager.planetSpeed);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyRay"))
        {
            planetHealth--;
            Instantiate(bulletEffect, other.gameObject.transform.position, Quaternion.identity).Play();
            StartCoroutine(PlanetShake());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlanetShake()
    {
        float elapsed = 0f;
        float duration = 0.01f;

        while (elapsed < duration)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        float elapsed2 = 0f;

        while (elapsed2 < duration)
        {
            transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);

            elapsed2 += Time.deltaTime;
            yield return null;
        }
        float elapsed3 = 0f;

        while (elapsed3 < duration)
        {
            transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);

            elapsed3 += Time.deltaTime;
            yield return null;
        }
        float elapsed4 = 0f;

        while (elapsed4 < duration)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

            elapsed4 += Time.deltaTime;
            yield return null;
        }
    }
}
