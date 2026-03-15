using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private TMP_Text FPSDisplay;

    private void Start()
    {
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
            FPSDisplay.SetText(Mathf.RoundToInt(count) + " FPS");

            yield return time;
        }
    }
}
