using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousEscape : MonoBehaviour
{
    public string scene;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(load());
    }

    IEnumerator load()
    {
        if (Fade.instance)
            Fade.instance.FadeIn(1);
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(scene);
    }
}
