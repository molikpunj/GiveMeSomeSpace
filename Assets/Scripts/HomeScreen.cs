using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private CanvasGroup blackScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TransitionOut(blackScreen));
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TransitionOut(CanvasGroup blackScreen)
    {
        yield return new WaitForSeconds(3);
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

    void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return StartCoroutine(TransitionIn(blackScreen));

        SceneManager.LoadScene("Campaign");
    }
}
