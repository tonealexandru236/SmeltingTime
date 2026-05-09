using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool canCraft;
    public bool canSmelt;

    private void Awake()
    {
        if(gameObject.name == "Player1")
        {
            if(PlayerPrefs.GetString("class-p1", "Craft") == "Craft")
            {
                canCraft = true;
                canSmelt = false;
            }
            else
            {
                canCraft = false;
                canSmelt = true;
            }
        }
        else
        {
            if (PlayerPrefs.GetString("class-p2", "Craft") == "Craft")
            {
                canCraft = true;
                canSmelt = false;
            }
            else
            {
                canCraft = false;
                canSmelt = true;
            }
        }
    }
}
