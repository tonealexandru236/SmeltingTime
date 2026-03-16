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
                    StartCoroutine(Item_fly_animation(closestStation.transform.position));
                closestStation.UseStation(this);
            }

            closestStation.ActivateStation(true, this);
        }
    }

    private float item_fly_speed = 1f;

    public IEnumerator Item_fly_animation(Vector3 to_pos)
    {
        throwable_item.GetComponent<SpriteRenderer>().sprite = itemInHandSprites[2];
        throwable_item.transform.position = gameObject.transform.position;
        throwable_item.SetActive(true);

        //to_pos.y += 3;
        float init_delta_x = Mathf.Abs(to_pos.x - throwable_item.transform.position.x);
        float go_to_y = Mathf.Max((throwable_item.transform.position.y + to_pos.y) / 2, gameObject.transform.position.y + 1);

        while (Vector3.Distance(to_pos, throwable_item.transform.position) > 0.05f)
        {
            if (Mathf.Abs(to_pos.x - throwable_item.transform.position.x) > init_delta_x * 3 / 4)
            {
                Vector3 pos = throwable_item.transform.position;
                pos.y += Mathf.Abs(go_to_y - pos.y) / 30;
                throwable_item.transform.position = pos;

                throwable_item.transform.position = Vector3.MoveTowards(throwable_item.transform.position, new Vector3(to_pos.x, throwable_item.transform.position.y, throwable_item.transform.position.z), item_fly_speed * Time.deltaTime / 5);
            }
            else
                throwable_item.transform.position = Vector3.MoveTowards(throwable_item.transform.position, to_pos, item_fly_speed * Time.deltaTime * Mathf.Max(Mathf.Abs(go_to_y - throwable_item.transform.position.y), 1.2f));


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
