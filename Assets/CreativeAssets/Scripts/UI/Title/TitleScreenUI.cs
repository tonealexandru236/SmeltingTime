using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject settings;
    public GameObject level_selector;

    public GameObject exitBg;

    public GameObject custom1;
    public GameObject custom2;

    private GameObject menu_active;

    [SerializeField]  public Slider masterSlider;

    public TMP_Text sliderPercentage;

    public Toggle fps;
    public Toggle weather;
    public Toggle particles;
    public Toggle buttons;
    public Toggle color;

    public Toggle fastmenus;
    public Toggle showversion;

    public AudioClip TitleTrack;

    void Awake()
    {
        Time.timeScale = 1;

        if (AudioManager.instance && AudioManager.instance.audioSources[11] != TitleTrack)
        {
            AudioManager.instance.audioSources[11].clip = TitleTrack;
            AudioManager.instance.audioSources[11].loop = true;
            AudioManager.instance.audioSources[11].pitch = 1;
            AudioManager.instance.audioSources[11].Play();
        }
    }

    public void change_color()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_color", color.isOn ? 1 : 0);
    }

    public void change_fps()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_fps", fps.isOn ? 1 : 0);
    }

    public void change_weather()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_weather", weather.isOn ? 1 : 0);
    }

    public void change_parts()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_particles", particles.isOn ? 1 : 0);
    }

    public void change_butts()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_buttons", buttons.isOn ? 1 : 0);
    }

    public void change_transitions()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_transition", fastmenus.isOn ? 3 : 1); ///Dace e on e de 2 ori mai rapid
    }

    public void change_version()
    {
        AudioManager.instance.PlaySound("buttonSelect");
        PlayerPrefs.SetInt("s_version", showversion.isOn ? 1 : 0); ///Dace e on e de 2 ori mai rapid
    }

    private void Start()
    {
        float value = PlayerPrefs.GetFloat("masterVolume", 1);
        masterSlider.value = value;
        if (sliderPercentage != null)  sliderPercentage.text = (Mathf.Round(value * 100)).ToString() + "%";

        if (fps != null) fps.isOn = PlayerPrefs.GetInt("s_fps", 0) == 1;
        if (weather != null) weather.isOn = PlayerPrefs.GetInt("s_weather", 1) == 1;
        if (particles != null) particles.isOn = PlayerPrefs.GetInt("s_particles", 1) == 1;
        if (buttons != null) buttons.isOn = PlayerPrefs.GetInt("s_buttons", 1) == 1;
        if (color != null) color.isOn = PlayerPrefs.GetInt("s_color", 0) == 1;

        if (showversion != null) showversion.isOn = PlayerPrefs.GetInt("s_version", 1) == 1;

        if (fastmenus != null)
        {
            if (PlayerPrefs.GetInt("s_transition", 1) == 3) fastmenus.isOn = true;
            else fastmenus.isOn = false;
        }
            
            

        StartCoroutine(BandaidFix());

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
    }

    IEnumerator BandaidFix()
    {
        yield return new WaitForSeconds(0.1f);
        //AudioManager.instance.PlayTrack("MainBass");
        if(AudioManager.instance != null) AudioManager.instance.PlayWeather("Rain", 0);
    }

    void OnMasterVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();

        if (sliderPercentage != null) sliderPercentage.text = Mathf.Round(value * 100) + "%";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && is_on)
        {
            if (menu_active == settings)
                click_settings();
            else if (menu_active == level_selector)
                click_levels();
            else if (menu_active == exitBg)
                click_exit();
        }

        /*PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
        PlayerPrefs.Save();
        if (sliderPercentage != null) sliderPercentage.text = (Mathf.Round(masterSlider.value * 100)).ToString() + "%";*/
    }

    IEnumerator animate_exit(int invers)
    {
        CanvasGroup cg = exitBg.GetComponent<CanvasGroup>();
        float speed = PlayerPrefs.GetInt("s_transition", 1);
        float duration = 0.09f / speed;
        float elapsed_time = 0f;

        float start_a = 1 - invers;
        float end_a = invers;

        while (elapsed_time < duration)
        {
            elapsed_time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start_a, end_a, elapsed_time / duration);
            yield return null;
        }

        cg.alpha = end_a;
    }

    public void click_levels()
    {
        AudioManager.instance.PlaySound("buttonClick");
        StartCoroutine(deactivate_ui(0.75f));

        Animator anim = level_selector.GetComponent<Animator>();

        if (menu_active == null)
        {
            anim.SetFloat("Speed", PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("levels", 0, 0);
            menu_active = level_selector;
        }
        else
        {
            anim.SetFloat("Speed", -PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("levels", 0, 1);
            menu_active = null;
        }
    }

    public void click_settings()
    {
        AudioManager.instance.PlaySound("buttonClick");
        StartCoroutine(deactivate_ui(0.5f));

        Animator anim = settings.GetComponent<Animator>();

        if (menu_active == null)
        {
            menu_active = settings;
            anim.SetFloat("Speed", PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("settings", 0, 0);
        }
        else
        {
            anim.SetFloat("Speed", -PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("settings", 0, 1);
            menu_active = null;
        }
    }

    public void click_exit()
    {
        AudioManager.instance.PlaySound("buttonClick");
        StartCoroutine(deactivate_ui(0.1f));

        if (menu_active == null)
        {
            StartCoroutine(animate_exit(1));
            menu_active = exitBg;
        }
        else
        {
            StartCoroutine(animate_exit(0));
            //anim.Play("exit-attempt", 0, 1);
            menu_active = null;
        }
    }

    public void click_c1()
    {
        AudioManager.instance.PlaySound("buttonClick");
        //StartCoroutine(deactivate_ui(0.25f));

        Animator anim = custom1.GetComponent<Animator>();

        Debug.Log(custom1.GetComponent<Image>().color.a);

        if (custom1.GetComponent<Image>().color.a <= 0.99)
        {
            anim.SetFloat("Speed", PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("customize", 0, 0);
        }
        else
        {
            Debug.Log("!");
            anim.SetFloat("Speed", -PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("customize", 0, 1);
        }
    }

    public void click_c2()
    {
        AudioManager.instance.PlaySound("buttonClick");
        //StartCoroutine(deactivate_ui(0.25f));

        Animator anim = custom2.GetComponent<Animator>();

        if (custom2.GetComponent<Image>().color.a <= 0.99)
        {
            anim.SetFloat("Speed", PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("customize", 0, 0);
        }
        else
        {
            anim.SetFloat("Speed", -PlayerPrefs.GetInt("s_transition", 1));
            anim.Play("customize", 0, 1);
        }
    }

    List<Button> save = new List<Button>();
    bool is_on = true;
    IEnumerator deactivate_ui(float wait)
    {
        is_on = false;
        foreach (Button but in FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            if(but.interactable == true)
            {
                save.Add(but);
                but.interactable = false;
            }
        }

        yield return new WaitForSeconds(wait/ PlayerPrefs.GetInt("s_transition", 1));

        foreach (Button but in save)
            but.interactable = true;

        is_on = true;
    }

}
