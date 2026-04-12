using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemSplashes : MonoBehaviour
{

    public void pick_up_animation(string player, string item_name, Sprite item_sprite)
    {
        
        if (player == "Player1" && gameObject.name == "P1-item")
        {
            update_info(item_name, item_sprite);
            gameObject.GetComponent<Animator>().Play("p1-item-splash-r", 0, 0);
        }
        else if (player == "Player2" && gameObject.name == "P2-item")
        {
            update_info(item_name, item_sprite);
            gameObject.GetComponent<Animator>().Play("p1-item-splash-l", 0, 0);
        }
    }

    public void pick_down_animation(string player, string item_name, Sprite item_sprite)
    {
        if (player == "Player1" && gameObject.name == "P1-item")
        {
            gameObject.GetComponent<Animator>().Play("p1-item-splash-l", 0, 0);
        }
        else if (player == "Player2" && gameObject.name == "P2-item")
        {
            gameObject.GetComponent<Animator>().Play("p1-item-splash-r", 0, 0);
        }
    }

    void update_width(string name)
    {
        RectTransform rect_t = gameObject.GetComponent<RectTransform>();
        rect_t.sizeDelta = new Vector2(Mathf.Max(100, 100 * name.Length / 18f), rect_t.sizeDelta.y);

        transform.GetChild(0).GetComponent<TMP_Text>().fontSize = 20 - name.Length/1.55f;

        Debug.Log(name.Length);
    }

    void update_info(string name, Sprite sprite)
    {
        if (name.StartsWith("-"))
            name = name.Substring(1);

        update_width(name);
        transform.GetChild(0).GetComponent<TMP_Text>().SetText(name);
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
    }
}
