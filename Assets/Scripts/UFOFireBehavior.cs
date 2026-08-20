using UnityEngine;

public class UFOFireBehavior : MonoBehaviour
{
    public GameManager GameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.down * 3 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gameOver = true;
            Instantiate(GameManager.explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
