using UnityEngine;

public class TableScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer mainSr;
    [SerializeField] SpriteRenderer bgSr;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] string itemStored;
    int enchantLvl;

    GameObject enchantFire;

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

        if (enchantFire)
            enchantFire.GetComponent<SpriteRenderer>().sortingOrder = mainSr.sortingOrder - 1;
    }
    public void UseTable(string itemID, PlayerHand ph, int enchLvl)
    {
        if(itemStored == "" && itemID != "")
        {
            itemStored = itemID;
            mainSr.sprite = itemDb.GetObjById(itemID).itemSprites[0];
            mainSr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 2;
            bgSr.sprite = null;
            //mainSr.sprite = itemDb.GetObjById(itemID).pickUpSprite;

            enchantLvl = enchLvl;

            if (enchantLvl != 0)
            {
                GameObject fire = Instantiate(FindFirstObjectByType<ItemDatabase>().firePref, transform);
                fire.GetComponent<EnchantFire>().SetUpFire(enchantLvl);
                fire.transform.parent = mainSr.transform;
                fire.transform.localPosition = new Vector2(0, 0);

                enchantFire = fire;
            }

            ph.RemoveItemInHand();
        }
        else if(itemStored != "" && itemID == "")
        {
            ph.PickUpItemInHand(itemDb.GetObjById(itemStored).itemSprites, itemStored, enchantLvl);
            Destroy(enchantFire);

            itemStored = "";
            mainSr.sprite = null;
            bgSr.sprite = arrowSprite;
        }
    }
}
