using UnityEngine;
using UnityEngine.UI;

public class SmithingTable : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] SpriteRenderer toSmithSr;
    [SerializeField] SpriteRenderer resultSr;
    [SerializeField] GameObject netheriteSprite;
    [SerializeField] Image circleImg;

    string itemToSmith, result, finalizedResult;
    bool hasNetherite;

    PlayerHand currentPlayerHand;
    KeyCode currentPlayerActionKey;

    float t;
    private void Start()
    {
        itemToSmith = "";
        result = "";
        finalizedResult = "";
        t = -1;
    }
    private void Update()
    {
        if(t >= 0)
        {
            t += Time.deltaTime;

            if (t > 0.5f)
            {
                if(result == "")
                {
                    t = -1;
                    return;
                }

                circleImg.fillAmount = t / 2f;
            }
            if(t >= 2f)
            {
                hasNetherite = false;
                itemToSmith = "";
                toSmithSr.sprite = null;
                netheriteSprite.SetActive(false);

                finalizedResult = result;

                resultSr.sprite = FindFirstObjectByType<ItemDatabase>().GetObjById(result).itemSprites[0];
            }

            if (Input.GetKeyUp(currentPlayerActionKey))
            {
                if(t <= 0.5f) {
                    if (finalizedResult != "")
                    {
                        currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById(finalizedResult).itemSprites, finalizedResult, 0);
                        finalizedResult = "";
                        resultSr.sprite = null;
                    }
                    else if (itemToSmith != "")
                    {
                        currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById(itemToSmith).itemSprites, itemToSmith, 0);
                        itemToSmith = "";
                        toSmithSr.sprite = null;
                    }
                    else if(hasNetherite)
                    {
                        currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById("netheriteingot").itemSprites, "netheriteingot", 0);
                        hasNetherite = false;
                        netheriteSprite.SetActive(false);
                    }
                }

                circleImg.fillAmount = 0;
                t = -1;
            }
        }
    }
    public void UseSmithingTable(string itemId, PlayerHand ph, KeyCode k)
    {
        if(itemId != "")
        {
            if(itemId == "netheriteingot")
            {
                hasNetherite = true;
                netheriteSprite.SetActive(true);
                ph.RemoveItemInHand();
            }
            else if(itemToSmith == "") {
                itemToSmith = itemId;
                toSmithSr.sprite = FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).itemSprites[0];
                ph.RemoveItemInHand();
            }
            

            print("netherite" + itemToSmith.Substring(7));
            if (hasNetherite && itemToSmith != "" && FindFirstObjectByType<ItemDatabase>().GetObjById("netherite" + itemToSmith.Substring(7)))
            {
                result = "netherite" + itemToSmith.Substring(7);
            }
        }
        else
        {
            t = 0;
            currentPlayerActionKey = k;
            currentPlayerHand = ph;
        }
    }
}
