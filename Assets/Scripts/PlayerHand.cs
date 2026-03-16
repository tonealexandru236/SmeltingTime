using System.Collections;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Sprite[] itemInHandSprites = new Sprite[0];
    public GameObject throwable_item;

    public string itemInHandID;
    void Start() {
        itemInHandID = "";
    }
    private void Update()
    {
        if(itemInHandID == "") {
            //Doesn't have item
            float disItem = -1;
            ItemScript closestItem = null;
            foreach(ItemScript itm in FindObjectsByType<ItemScript>(FindObjectsSortMode.None)) {
                itm.SetInPickUpRange(false);

                float dis_to_item = Vector2.Distance(transform.position, itm.transform.position);

                if(dis_to_item < 1f && (disItem == -1 || dis_to_item < disItem))
                {
                    disItem = dis_to_item;
                    closestItem = itm;
                }
            }
            if (closestItem != null) closestItem.SetInPickUpRange(true);

            if(Input.GetKeyDown(KeyCode.F)) {
                if (closestItem != null && itemInHandID == "") {
                    itemInHandSprites = closestItem.itemSprites;
                    itemInHandID = closestItem.PickUpItem();
                    Debug.Log(itemInHandID);

                    return;
                }
            }
        }
        float disStation = -1;
        StationScript closestStation = null;
        foreach(StationScript station in FindObjectsByType<StationScript>(FindObjectsSortMode.None)) {
            float d = Vector2.Distance(transform.position, station.transform.position);

            station.ActivateStation(false, this);

            if(d < 1.3f && (disStation == -1 || d < disStation))
            {
                disStation = d;
                closestStation = station;
            }
        }

        if(closestStation != null) { /// DACA ESTE O STATIE IN RANGE
            if(Input.GetKeyDown(KeyCode.F))
                closestStation.UseStation(this);

            closestStation.ActivateStation(true, this);
        }
    }

    private float item_fly_speed = 1f;

    

    public void SetOrientationForItemInHand(int q)
    {
        if (itemInHandID == "") return;
        GetComponent<SpriteRenderer>().sprite = itemInHandSprites[q];
    }

    public void RemoveItemInHand() {
        itemInHandID = "";
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public void PickUpItemInHand(Sprite[] spr, string itemId) {
        itemInHandID = itemId;
        itemInHandSprites = spr;
    }
}
