using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnHeight;
    [SerializeField] public float planetSpeed;
    public int destroyedCount;
    public bool gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnPlanet()
    {
        while (!gameOver)
        {
            float spawnPoint = Random.Range(-spawnRange, spawnRange);
            Instantiate(planets[Random.Range(0, 5)], new Vector3(spawnPoint, spawnHeight, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 6));
        }
    }
}
