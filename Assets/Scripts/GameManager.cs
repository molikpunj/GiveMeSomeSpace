using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;
    [SerializeField] private GameObject[] ufos;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnHeight;
    [SerializeField] public float planetSpeed;
    public int destroyedCount;
    public bool gameOver;
    public bool planetSpawnWave = true;
    public bool ufoSpawnWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnPlanet());
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectsByType<UFOBehavior>(FindObjectsSortMode.None).Length == 0 && !planetSpawnWave && ufoSpawnWave)
        {
            destroyedCount = 0;
            ufoSpawnWave = false;
            planetSpawnWave = true;
            StartCoroutine(spawnPlanet());
        }
    }
    IEnumerator spawnPlanet()
    {
        while(destroyedCount < 1 && !gameOver && planetSpawnWave)
        {
            float spawnPoint = Random.Range(-spawnRange, spawnRange);
            Instantiate(planets[Random.Range(0, 5)], new Vector3(spawnPoint, spawnHeight, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
        planetSpawnWave = false;
        yield return new WaitUntil(() => FindObjectsByType<PlanetsSpin>(FindObjectsSortMode.None).Length == 0);
        ufoSpawnWave = true;
        spawnUFO();
    }

    void spawnUFO()
    {
        float spawnpointx = -3.6f;
        for(int i = 0; i <= 4; i++)
        {
            Instantiate(ufos[Random.Range(0, 5)], new Vector3(spawnpointx, 0, 0), ufos[0].gameObject.transform.rotation);
            spawnpointx += 1.8f;
        }
    }
}
