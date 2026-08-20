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
            GameManager.aliensKilled += Random.Range(1000, 30000);
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
        for(int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
