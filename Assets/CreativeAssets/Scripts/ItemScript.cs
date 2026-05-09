using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float getMadTime;

    public Sprite[] itemSprites;

    public string itemId;
    public string recipeForThisItem;

    public int enchantLevel;

    [Header("Visual")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite normalSprite;
    public Sprite pickUpSprite;

    public bool is8dir;

    SpriteRenderer enchantFire;

    private void Start()
    {
        if (enchantLevel != 0)
        {
            GameObject fire =  Instantiate(FindFirstObjectByType<ItemDatabase>().firePref, transform);
            fire.GetComponent<EnchantFire>().SetUpFire(enchantLevel);

            fire.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
            
    }
    void Update() {

        if(transform.position.y < -16) ///Despawn
            Destroy(gameObject);
    }
    public void SetInPickUpRange(bool canBePickedUp)
    {
         sr.sprite = canBePickedUp ? pickUpSprite : normalSprite;
    }
    public string PickUpItem(GameObject player)
    {
        if(AudioManager.instance)
            AudioManager.instance.PlaySound("itemPick");
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
                splash.pick_up_animation(player.name, FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).name.Substring(5), pickUpSprite);

        Destroy(gameObject);
        return itemId;
    }
}
