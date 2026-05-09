using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public string scene_name;
    public void on_click()
    {
        AudioManager.instance.PlaySound("buttonClick");

        StartCoroutine(do_anim());
    }

    IEnumerator do_anim()
    {
        if (Fade.instance)
            Fade.instance.FadeIn(1);
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(scene_name);
    }
}
