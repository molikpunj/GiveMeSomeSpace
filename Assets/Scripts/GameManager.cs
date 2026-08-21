using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;
    [SerializeField] private GameObject[] ufos;
    [SerializeField] public GameObject explosion;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnHeight;
    [SerializeField] public float planetSpeed;
    [SerializeField] public TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI waveScore;
    [SerializeField] private CanvasGroup restartScreen;
    [SerializeField] private Button restartButton;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private GameObject astraunotCutscene;
    [SerializeField] private GameObject alienCutscene1;
    [SerializeField] private GameObject alienCutscene2;
    [SerializeField] private GameObject alienCutscene3;
    private PlayerMovements playerMovements;
    public int destroyedCount;
    public bool gameOver;
    public bool planetSpawnWave = true;
    public bool ufoSpawnWave;
    public int aliensKilled;
    public int wavesDefeated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        playerMovements = GameObject.FindAnyObjectByType<PlayerMovements>();
        yield return StartCoroutine(TransitionOut(blackScreen));
        yield return StartCoroutine(ShowCutScene(astraunotCutscene));
        StartCoroutine(spawnPlanet());
        restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsByType<UFOBehavior>(FindObjectsSortMode.None).Length == 0 && !planetSpawnWave && ufoSpawnWave)
        {
            StartCoroutine(UFOWaveFinished());
        }
        if (gameOver && restartScreen.alpha < 1)
        {
            restartScreen.gameObject.SetActive(true);
            restartScreen.alpha += 0.5f * Time.deltaTime;
        }
    }
    IEnumerator spawnPlanet()
    {
        while(destroyedCount < 15 && !gameOver && planetSpawnWave)
        {
            float spawnPoint = Random.Range(-spawnRange, spawnRange);
            Instantiate(planets[Random.Range(0, 5)], new Vector3(spawnPoint, spawnHeight, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
        planetSpawnWave = false;
        yield return new WaitUntil(() => FindObjectsByType<PlanetsSpin>(FindObjectsSortMode.None).Length == 0);
        if (wavesDefeated == 0)
        {
            yield return StartCoroutine(ShowCutScene(alienCutscene1));
        }
        ufoSpawnWave = true;
        spawnUFO();
    }

    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        yield return StartCoroutine(TransitionIn(blackScreen));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    IEnumerator TransitionOut(CanvasGroup blackScreen)
    {
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            blackScreen.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        blackScreen.alpha = 0f;
        blackScreen.gameObject.SetActive(false);
    }

    IEnumerator TransitionIn(CanvasGroup blackScreen)
    {
        blackScreen.gameObject.SetActive(true);

        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            blackScreen.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    IEnumerator ShowCutScene(GameObject prefab)
    {
        GameObject cutscene = Instantiate(prefab);
        while(cutscene.transform.position.x < 0)
        {
            cutscene.transform.Translate(Vector3.right * 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => playerMovements.playerInput.Player.Touch.WasPressedThisFrame());
        while (cutscene.transform.position.x < 10)
        {
            cutscene.transform.Translate(Vector3.right * 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(cutscene);
    }

    IEnumerator UFOWaveFinished()
    {
        ufoSpawnWave = false;

        wavesDefeated++;
        waveScore.text = $"{wavesDefeated}";

        if (wavesDefeated == 1)
        {
            yield return StartCoroutine(ShowCutScene(alienCutscene2));
        }
        else if (wavesDefeated >= 2)
        {
            yield return StartCoroutine(ShowCutScene(alienCutscene3));
        }

        destroyedCount = 0;
        planetSpawnWave = true;

        StartCoroutine(spawnPlanet());
    }
}
