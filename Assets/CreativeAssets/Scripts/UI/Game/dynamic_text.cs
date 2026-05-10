using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class dynamic_text : MonoBehaviour
{
    public int type; /// 1 = Crafter   2 = Smelter
    public GameObject p1;
    public GameObject p2;
    void Start()
    {
        Color col = Color.white;
        string cur_color = "";

        string[] words = GetComponent<TMP_Text>().text.Split(' ');

        if (p1.GetComponent<PlayerScript>().canSmelt)
        {
            cur_color = PlayerPrefs.GetString("p1", "FF0000");
            words[0] = "P1";
        }
        else if(p2.GetComponent<PlayerScript>().canSmelt)
        {
            cur_color = PlayerPrefs.GetString("p2", "00CDFF");
            words[0] = "P2";
        }

        cur_color = "#" + cur_color;
        if (ColorUtility.TryParseHtmlString(cur_color, out col))
        {
            GetComponent<TMP_Text>().color = col;
        }


        string result = string.Join(" ", words);

        GetComponent<TMP_Text>().SetText(result);
    }
}
