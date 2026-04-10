using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private TMP_Text FPSDisplay;
    private int MaxRefreshRate;

    private void Start()
    {
        double hz = Screen.currentResolution.refreshRateRatio.value;
        MaxRefreshRate = (int)Math.Round(hz);

        FPSDisplay = GetComponent<TMP_Text>();
        StartCoroutine(Track_FPS());
    }

    private float count;

    private IEnumerator Track_FPS()
    {
        WaitForSeconds time = new WaitForSeconds(1);

        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            FPSDisplay.SetText(Mathf.Min(MaxRefreshRate, Mathf.RoundToInt(count)) + " FPS");

            yield return time;
        }
    }
}
