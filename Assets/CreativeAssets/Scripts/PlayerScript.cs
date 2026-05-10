using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public bool canCraft;
    public bool canSmelt;

    private void Awake()
    {
        if (gameObject.name == "Player1")
        {
            if(PlayerPrefs.GetString("class-p1", "Craft") == "Craft")
            {
                canCraft = true;
                canSmelt = false;

                GetComponent<PlayerMovement>().SetPlayerFit(true);
            }
            else
            {
                canCraft = false;
                canSmelt = true;

                GetComponent<PlayerMovement>().SetPlayerFit(false);
            }
        }
        else
        {
            if (PlayerPrefs.GetString("class-p2", "Craft") == "Craft")
            {
                canCraft = true;
                canSmelt = false;

                GetComponent<PlayerMovement>().SetPlayerFit(true);
            }
            else
            {
                canCraft = false;
                canSmelt = true;

                GetComponent<PlayerMovement>().SetPlayerFit(false);
            }
        }

        if(SceneManager.GetActiveScene().name == "Tutorial")
            canCraft = true;
    }
}
