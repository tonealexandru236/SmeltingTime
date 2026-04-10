using UnityEngine;

public class TableScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer mainSr;
    [SerializeField] SpriteRenderer bgSr;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] string itemStored;

    ItemDatabase itemDb;
    float t = 0;
    private void Start()
    {
        itemStored = "";
        itemDb = FindFirstObjectByType<ItemDatabase>();
    }
    private void Update()
    {
        t += 2 * Time.deltaTime;
        t %= 2;

        if (t <= 1) mainSr.transform.localPosition = new Vector3(0, 0.15f + 0.13f * t, 0);
        else mainSr.transform.localPosition = new Vector3(0, 0.28f - 0.13f * (t - 1f), 0);
    }
    public void UseTable(string itemID, PlayerHand ph)
    {
        if(itemStored == "" && itemID != "")
        {
            itemStored = itemID;
            mainSr.sprite = itemDb.GetObjById(itemID).itemSprites[0];
            mainSr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            bgSr.sprite = null;
            //mainSr.sprite = itemDb.GetObjById(itemID).pickUpSprite;

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
