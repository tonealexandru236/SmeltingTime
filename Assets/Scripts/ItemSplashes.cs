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
            Debug.Log("OK");
            gameObject.GetComponent<Animator>().Play("p1-item-splash-l", 0, 0);
        }
        else if (player == "Player2" && gameObject.name == "P2-item")
            gameObject.GetComponent<Animator>().Play("p1-item-splash-r", 0, 0);
    }

    void update_info(string name, Sprite sprite)
    {
        transform.GetChild(0).GetComponent<TMP_Text>().SetText(name);
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
    }
}
