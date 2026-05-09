using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    public Image fade_bg;

    void Awake()
    {
        /// Singleton thingy again
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("more than 1 fade thingies!!");
            Destroy(gameObject);
        }
    }

    public void FadeOut(float speed)
    {
        Animator bg = fade_bg.GetComponent<Animator>();

        bg.SetFloat("Speed", speed * PlayerPrefs.GetInt("s_transition", 1));
        bg.Play("fade", 0, 0);
    }

    public void FadeIn(float speed)
    {
        Animator bg = fade_bg.GetComponent<Animator>();

        bg.SetFloat("Speed", -speed * PlayerPrefs.GetInt("s_transition", 1));
        bg.Play("fade", 0, 1);
    }
}
