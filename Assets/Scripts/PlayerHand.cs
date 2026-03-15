using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    Sprite[] itemInHandSprites;

    private void Update()
    {
        float dis = -1;
        ItemScript closestItem = null;
        foreach(ItemScript itm in FindObjectsByType<ItemScript>(FindObjectsSortMode.None)) {
            itm.SetInPickUpRange(false);

            if(Vector2.Distance(transform.position, itm.transform.position) < 1f && (dis == -1 || Vector2.Distance(transform.position, itm.transform.position) < dis))
            {
                dis = Vector2.Distance(transform.position, itm.transform.position);
                closestItem = itm;
            }
        }
        if (closestItem != null) closestItem.SetInPickUpRange(true);

        if(Input.GetKeyDown(KeyCode.F)) {
            if (closestItem == null) return;

            itemInHandSprites = closestItem.itemSprites;

            closestItem.PickUpItem();
        }
    }
    public void SetOrientationForItemInHand(int q)
    {
        if (itemInHandSprites.Length < 4) return;
        GetComponent<SpriteRenderer>().sprite = itemInHandSprites[q];
    }
}
