using System.Collections;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ufoFire;
    public GameManager GameManager;
    private float rotationDirection;
    private int health;
    private int sittingPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        sittingPoint = Random.Range(-3, -9);
        health = Random.Range(20, 31);
        rotationDirection = Random.Range(-50, 50);
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
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
            Destroy(other.gameObject);
        }
    }

    IEnumerator shoot()
    {
        while(!GameManager.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(4, 7));
            Instantiate(ufoFire, transform.position, ufoFire.transform.rotation);
        }
    }
}
