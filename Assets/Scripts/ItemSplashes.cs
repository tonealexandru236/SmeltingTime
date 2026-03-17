using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemSplashes : MonoBehaviour
{

    public void pick_up_animation(GameObject player, string item_name, Sprite item_sprite)
    {
        update_info();
        if (player.name == "Player1" && gameObject.name == "P1-item")
            gameObject.GetComponent<Animator>().Play("p1-item-splash-r", 0, 0);
        else if (player.name == "Player2" && gameObject.name == "P2-item")
            gameObject.GetComponent<Animator>().Play("p1-item-splash-l", 0, 0);
    }

    public void pick_down_animation(GameObject player, string item_name, Sprite item_sprite)
    {
        Debug.Log(player.name);
        if (player.name == "Player1" && gameObject.name == "P1-item")
        {
            Debug.Log("OK");
            gameObject.GetComponent<Animator>().Play("p1-item-splash-l", 0, 0);
        }
        else if (player.name == "Player2" && gameObject.name == "P2-item")
            gameObject.GetComponent<Animator>().Play("p1-item-splash-r", 0, 0);
    }

    void update_info()
    {

    }
}
