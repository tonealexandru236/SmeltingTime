using UnityEngine;

public class TableScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer mainSr;
    [SerializeField] SpriteRenderer bgSr;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] string itemStored;

    ItemDatabase itemDb;
    private void Start()
    {
        itemStored = "";
        itemDb = FindFirstObjectByType<ItemDatabase>();
    }
    public void UseTable(string itemID, PlayerHand ph)
    {
        if(itemStored == "" && itemID != "")
        {
            itemStored = itemID;
            mainSr.sprite = itemDb.GetObjById(itemID).itemSprites[0];
            bgSr.sprite = itemDb.GetObjById(itemID).pickUpSprite;

            ph.RemoveItemInHand();
        }
        else if(itemStored != "" && itemID == "")
        {
            ph.PickUpItemInHand(itemDb.GetObjById(itemStored).itemSprites, itemStored);

            itemStored = "";
            mainSr.sprite = null;
            bgSr.sprite = arrowSprite;
        }
    }
}
