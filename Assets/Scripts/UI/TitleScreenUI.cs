using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject settings;
    public GameObject level_selector;

    public GameObject custom1;
    public GameObject custom2;

    private GameObject menu_active;

    public Slider masterSlider;

    public TMP_Text sliderPercentage;

    public Toggle fps;
    public Toggle weather;
    public Toggle particles;
    public Toggle buttons;

    void Awake()
    {
        Time.timeScale = 1;

        fps.isOn = PlayerPrefs.GetInt("s_fps", 0) == 1;
        weather.isOn = PlayerPrefs.GetInt("s_weather", 1) == 1;
        particles.isOn = PlayerPrefs.GetInt("s_particles", 1) == 1;
        buttons.isOn = PlayerPrefs.GetInt("s_buttons", 1) == 1;
    }

    public void change_fps()
    {
        PlayerPrefs.SetInt("s_fps", fps.isOn ? 1 : 0);
    }

    public void change_weather()
    {
        PlayerPrefs.SetInt("s_weather", weather.isOn ? 1 : 0);
    }

    public void change_parts()
    {
        PlayerPrefs.SetInt("s_particles", particles.isOn ? 1 : 0);
    }

    public void change_butts()
    {
        PlayerPrefs.SetInt("s_buttons", buttons.isOn ? 1 : 0);
    }

    private void Start()
    {
        AudioManager.instance.PlayTrack("MainBass");

        float value = PlayerPrefs.GetFloat("masterVolume", 1);

        masterSlider.value = value;
        sliderPercentage.text = (Mathf.Round(value * 100)).ToString() + "%";
    }

    void Update()
    {
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
        sliderPercentage.text = (Mathf.Round(masterSlider.value * 100)).ToString() + "%";
    }

    public void click_levels()
    {
        StartCoroutine(deactivate_ui(0.75f));

        Animator anim = level_selector.GetComponent<Animator>();

        if (menu_active == null)
        {
            anim.SetFloat("Speed", 1);
            anim.Play("levels", 0, 0);
            menu_active = level_selector;
        }
        else
        {
            anim.SetFloat("Speed", -1);
            anim.Play("levels", 0, 1);
            menu_active = null;
        }
    }

    public void click_settings()
    {
        StartCoroutine(deactivate_ui(0.5f));

        Animator anim = settings.GetComponent<Animator>();

        if (menu_active == null)
        {
            anim.SetFloat("Speed", 1);
            anim.Play("settings", 0, 0);
            menu_active = settings;
        }
        else
        {
            anim.SetFloat("Speed", -1);
            anim.Play("settings", 0, 1);
            menu_active = null;
        }
    }

    public void click_c1()
    {
        //StartCoroutine(deactivate_ui(0.25f));

        Animator anim = custom1.GetComponent<Animator>();

        Debug.Log(custom1.GetComponent<Image>().color.a);

        if (custom1.GetComponent<Image>().color.a <= 0.99)
        {
            anim.SetFloat("Speed", 1);
            anim.Play("customize", 0, 0);
        }
        else
        {
            Debug.Log("!");
            anim.SetFloat("Speed", -1);
            anim.Play("customize", 0, 1);
        }
    }

    public void click_c2()
    {
        //StartCoroutine(deactivate_ui(0.25f));

        Animator anim = custom2.GetComponent<Animator>();

        if (custom2.GetComponent<Image>().color.a <= 0.99)
        {
            anim.SetFloat("Speed", 1);
            anim.Play("customize", 0, 0);
        }
        else
        {
            anim.SetFloat("Speed", -1);
            anim.Play("customize", 0, 1);
        }
    }

    List<Button> save = new List<Button>();
    IEnumerator deactivate_ui(float wait)
    {
        foreach (Button but in FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            if(but.interactable == true)
            {
                save.Add(but);
                but.interactable = false;
            }
        }

        yield return new WaitForSeconds(wait);

        foreach (Button but in save)
            but.interactable = true;
    }

}
