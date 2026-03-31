using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    public int player;
    private Color color;
    public void set()
    {
        color = GetComponent<Image>().color;

        if (player == 1)
        {
            string other = PlayerPrefs.GetString("p2");

            if(other != color.ToHexString())
                PlayerPrefs.SetString("p1", color.ToHexString());
        }
        else if (player == 2)
        {
            string other = PlayerPrefs.GetString("p1");

            if (other != color.ToHexString())
                PlayerPrefs.SetString("p2", color.ToHexString());
        }
    }

    public void Update()
    {
        //Debug.Log(GetComponent<Image>().color.a);
        if (GetComponent<Image>().color.a < 1)
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().raycastTarget = true;
        }

    }
}
