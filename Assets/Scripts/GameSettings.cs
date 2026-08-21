using UnityEngine;

public class GameSettings : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = Mathf.RoundToInt(
            (float)Screen.currentResolution.refreshRateRatio.value
        );
    }
}
