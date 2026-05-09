using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause_menu;
    public TMP_Text level;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pause_menu.activeSelf == true)
        {
            pause_menu.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pause_menu.activeSelf == false)
        {
            Time.timeScale = 0.0f;
            pause_menu.SetActive(true);
        }
    }

    private void Start()
    {
        level.SetText(SceneManager.GetActiveScene().name);
    }
}
