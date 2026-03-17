using System.Collections;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public KeyCode playerActionKey;
    public Sprite[] itemInHandSprites = new Sprite[0];
    public string itemInHandID;
    public GameObject player;
    public int playerPriority;
    public PlayerMovement pm;

    void Start() {
        player = transform.parent.gameObject;
        itemInHandID = "";
    }
    public void ManualUpdate()
    {
        if (itemInHandID == "") {
            //Doesn't have item
            RaycastHit2D itemHit = Physics2D.BoxCast(pm.transform.position, new Vector2(.5f, .5f), 0, (transform.position - pm.transform.position), 1.3f, LayerMask.GetMask("Item"));
            if (itemHit.collider != null) /// DACA ESTE O STATIE IN RANGE
            {
                ItemScript itm = itemHit.collider.GetComponent<ItemScript>();
                itm.SetInPickUpRange(true);
                if (Input.GetKeyDown(playerActionKey))
                {
                    itemInHandSprites = itm.itemSprites;
                    itemInHandID = itm.PickUpItem(player);
                    Debug.Log(itemInHandID);

                    return;
                }
            }
        }

        #region Find And Use Closest Station
        //Deactivate All Stations
        foreach (StationScript station in FindObjectsByType<StationScript>(FindObjectsSortMode.None))
            station.ActivateStation(false, this);

        RaycastHit2D hit = Physics2D.Raycast(pm.transform.position, (transform.position - pm.transform.position), 1.3f, LayerMask.GetMask("Station"));
        if (hit.collider != null) /// DACA ESTE O STATIE IN RANGE
        {
            if (Input.GetKeyDown(playerActionKey))
                hit.collider.GetComponent<StationScript>().UseStation(this);

            hit.collider.GetComponent<StationScript>().ActivateStation(true, this);
        }
        #endregion
    }


    public void SetOrientationForItemInHand(int q)
    {
        if (itemInHandID == "") return;
        GetComponent<SpriteRenderer>().sprite = itemInHandSprites[q];
    }

    public void RemoveItemInHand() {
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_down_animation(player.name, itemInHandID, GetComponent<SpriteRenderer>().sprite);

        itemInHandID = "";
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public void PickUpItemInHand(Sprite[] spr, string itemId) {
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_up_animation(player.name, itemId, spr[1]); /// TO DO

        itemInHandID = itemId;
        itemInHandSprites = spr;
    }
}
