using System.Collections;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ufoFire;
    [SerializeField] private ParticleSystem bulletEffect;
    public GameManager GameManager;
    private float rotationDirection;
    private int health;
    private int sittingPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        sittingPoint = Random.Range(-4, -9);
        health = Random.Range(20, 31);
        rotationDirection = Random.Range(-50, 50);
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(GameManager.explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (transform.position.y > sittingPoint)
            {
                transform.Translate(Vector3.down * 3 * Time.deltaTime, Space.World);
            }
        transform.Rotate(0, 0, rotationDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyRay"))
        {
            health--;
            Instantiate(bulletEffect, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.7f, other.gameObject.transform.position.z - 0.7f), Quaternion.identity).Play();
            StartCoroutine(UFOShake());
            Destroy(other.gameObject);
        }
    }

    IEnumerator shoot()
    {
        while(!GameManager.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(4, 7));
            if (!GameManager.gameOver)
            {
                Instantiate(ufoFire, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), ufoFire.transform.rotation);
                StartCoroutine(Recoil());
            }
        }
    }

    IEnumerator Recoil()
    {
        for (int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator UFOShake()
    {
        for (int i = 1; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
