using System.Collections;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    Sprite[] itemInHandSprites = new Sprite[0];
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

                    return;
                }
            }
        }
        float disStation = -1;
        StationScript closestStation = null;
        foreach(StationScript station in FindObjectsByType<StationScript>(FindObjectsSortMode.None)) {
            float d = Vector2.Distance(transform.position, station.transform.position);

            station.ActivateStation(false, this);

            if(d < 1.25f && (disStation == -1 || d < disStation))
            {
                disStation = d;
                closestStation = station;
            }
        }

        if(closestStation != null) { /// DACA ESTE O STATIE IN RANGE
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (closestStation.gameObject.name == "TrashCan")
                    StartCoroutine(Item_fly_animation(closestStation.transform.position, 0.8f, 1.6f));
                //else if (closestStation.gameObject.name == "CraftingTable")
                //    StartCoroutine(Item_fly_animation(closestStation.transform.position, 0.5f, 1f));
                closestStation.UseStation(this);
            }

            closestStation.ActivateStation(true, this);
        }
    }

    private float item_fly_speed = 1f;

    public IEnumerator Item_fly_animation(Vector3 to_pos, float duration, float arc_height)
    {
        throwable_item.GetComponent<SpriteRenderer>().sprite = itemInHandSprites[2];
        throwable_item.transform.position = gameObject.transform.position;
        throwable_item.SetActive(true);

        Vector3 start_pos = transform.position;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            Vector3 cur_pos = Vector3.Lerp(start_pos, to_pos, t);

            cur_pos.y += arc_height * t * (1 - t) * 4;
            cur_pos.z = -1;

            throwable_item.transform.position = cur_pos;

            yield return null;
        }

        throwable_item.SetActive(false);
    }
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
