using System.Collections;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public KeyCode playerActionKey;
    public Sprite[] itemInHandSprites = new Sprite[0];
    public string itemInHandID;
    public int itemInHandEnchantmentLevel;
    public GameObject player;
    public int playerPriority;
    public PlayerMovement pm;

    [SerializeField] Sprite[] fireAnim;
    GameObject enchantFire;

    [SerializeField] bool showIn8Dir;
    ItemDatabase itemDb;

    PlayerScript ps;

    void Start() {
        itemDb = FindFirstObjectByType<ItemDatabase>();
        ps = transform.parent.GetComponent<PlayerScript>();
        
        player = transform.parent.gameObject;
        itemInHandID = "";
    }
    public void ManualUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = transform.parent.GetComponent<PlayerMovement>().playerSr.sortingOrder + (transform.position.y <= transform.parent.position.y ? 1 : -1);
        if (enchantFire)
            enchantFire.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;


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

                    itemInHandEnchantmentLevel = itm.enchantLevel;

                    if (itemInHandEnchantmentLevel != 0)
                    {
                        GameObject fire = Instantiate(FindFirstObjectByType<ItemDatabase>().firePref, transform);
                        fire.GetComponent<EnchantFire>().SetUpFire(itemInHandEnchantmentLevel);

                        enchantFire = fire;
                    }
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
            if (!ps.canCraft && hit.collider.GetComponent<StationScript>().stationTag == "crafting" ||
                !ps.canSmelt && hit.collider.GetComponent<StationScript>().stationTag == "furnace")
                return;

            if (itemInHandEnchantmentLevel != 0 && (
                hit.collider.GetComponent<StationScript>().stationTag == "crafting" || hit.collider.GetComponent<StationScript>().stationTag == "furnace" || hit.collider.GetComponent<StationScript>().stationTag == "enchant"))
                return;


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
        if (AudioManager.instance)
            AudioManager.instance.PlaySound("itemPlace");
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_down_animation(player.name, itemInHandID, GetComponent<SpriteRenderer>().sprite);

        itemInHandID = "";
        itemInHandEnchantmentLevel = 0;
        GetComponent<SpriteRenderer>().sprite = null;

        if(enchantFire)
            Destroy(enchantFire.gameObject);
    }

    public void PickUpItemInHand(Sprite[] spr, string itemId, int enchantLvl) {
        if (AudioManager.instance)
            AudioManager.instance.PlaySound("itemPick");
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
            splash.pick_up_animation(player.name, FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).name.Substring(4), spr[1]); /// TO DO

        itemInHandID = itemId;
        itemInHandSprites = spr;
        itemInHandEnchantmentLevel = enchantLvl;

        showIn8Dir = itemDb.GetObjById(itemId).is8dir;

        if(itemInHandEnchantmentLevel != 0)
        {
            GameObject fire = Instantiate(FindFirstObjectByType<ItemDatabase>().firePref, transform);
            fire.GetComponent<EnchantFire>().SetUpFire(itemInHandEnchantmentLevel);

            enchantFire = fire;
        }
    }
}
