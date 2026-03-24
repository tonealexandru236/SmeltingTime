using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject settings;
    public GameObject level_selector;

    private GameObject menu_active;

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
