using System.Collections;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        StartCoroutine(quit());
    }

    IEnumerator quit()
    {
        if (Fade.instance)
            Fade.instance.FadeIn(1);

        yield return new WaitForSeconds(1);

        Application.Quit();
    }
}
