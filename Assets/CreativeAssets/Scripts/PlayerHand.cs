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

    [SerializeField] bool showIn8Dir;
    ItemDatabase itemDb;
    void Start() {
        itemDb = FindFirstObjectByType<ItemDatabase>();
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
                    showIn8Dir = itm.is8dir;
                    //Debug.Log(itemInHandID);
                }

                return;
            }
        }

        #region Check if its looking at a customer
        //Deactivate all lines
        foreach (CustomersOrderLine customer in FindObjectsByType<CustomersOrderLine>(FindObjectsSortMode.None))
            customer.ActivateCustomer(null, this);

        RaycastHit2D customerHit = Physics2D.Raycast(pm.transform.position, (transform.position - pm.transform.position), 1.3f, LayerMask.GetMask("Customer"));
        if (customerHit.collider != null) /// DACA ESTE O STATIE IN RANGE
        {
            customerHit.collider.GetComponent<CustomersOrderLine>().ActivateCustomer(itemDb.GetObjById(itemInHandID), this);

            if(Input.GetKeyDown(playerActionKey))
                customerHit.collider.GetComponent<CustomersOrderLine>().GiveItemtoCustomer(itemDb.GetObjById(itemInHandID), this);
            return;
        }
        #endregion

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

        int indx = q % 4 == 0 ? 1 : (q % 2 == 0 ? 0 : (q == 1 || q == 5 ? 3 : 2));
        GetComponent<SpriteRenderer>().sprite = itemInHandSprites[indx];

        if(showIn8Dir)
            transform.localScale = new Vector2(q > 3 ? -1 : 1, q > 3 ? -1 : 1);
        else
            transform.localScale = new Vector2(1, 1);
    }

    public void RemoveItemInHand() {
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_down_animation(player.name, itemInHandID, GetComponent<SpriteRenderer>().sprite);

        itemInHandID = "";
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public void PickUpItemInHand(Sprite[] spr, string itemId) {
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_up_animation(player.name, FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).name.Substring(4), spr[1]); /// TO DO

        itemInHandID = itemId;
        itemInHandSprites = spr;

        showIn8Dir = itemDb.GetObjById(itemId).is8dir;
    }
}
