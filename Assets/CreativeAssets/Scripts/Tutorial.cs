using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TMP_Text text;
    void Update()
    {
        text.SetText("Welcome to Smelting Time. \n\n You and your friend are operating a factory (a surprisingly small one) and your job is to manufacture items and sell them to people! \n\n Your boss is watching you, so don't slack off or you'll both lose! Finish the player goal to win the game.");
        Time.timeScale = 0f;
    }

    public void understood()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
