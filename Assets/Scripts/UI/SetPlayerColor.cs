using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetPlayerColor : MonoBehaviour
{
    private string cur_col;
    public int player;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
            StartCoroutine(repeat());
        else
            assign();
    }

    IEnumerator repeat()
    {
        WaitForEndOfFrame time = new WaitForEndOfFrame();
        while (true)
        {
            assign();
            yield return time;
        }
    }


    void assign()
    {
        if(player == 1)
            cur_col = PlayerPrefs.GetString("p1", "FF0000");
        else if(player == 2)
            cur_col = PlayerPrefs.GetString("p2", "0500FF");

        Color color;
        if (GetComponent<Image>() != null)
        {
            cur_col = "#" + cur_col;
            if (ColorUtility.TryParseHtmlString(cur_col, out color))
            {
                color.a = 0.3f;
                GetComponent<Image>().color = color;
            }
        }
        else if (GetComponent<SpriteRenderer>() != null)
        {
            cur_col = "#" + cur_col;
            if (ColorUtility.TryParseHtmlString(cur_col, out color))
            {
                color.a = 1f;
                GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
