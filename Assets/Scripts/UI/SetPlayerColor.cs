using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerColor : MonoBehaviour
{
    private string cur_col;
    public int player;

    void Update()
    {
        if(player == 1)
            cur_col = PlayerPrefs.GetString("p1");
        else if(player == 2)
            cur_col = PlayerPrefs.GetString("p2");

        Color color;
        if (GetComponent<Image>() != null)
        {
            cur_col = "#" + cur_col;
            if (ColorUtility.TryParseHtmlString(cur_col, out color))
            {
                GetComponent<Image>().color = color;
            }
        }
    }
}
