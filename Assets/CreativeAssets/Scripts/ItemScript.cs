using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float getMadTime;

    public Sprite[] itemSprites;

    public string itemId;
    public string recipeForThisItem;

    [Header("Visual")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite normalSprite;
    public Sprite pickUpSprite;

    public bool is8dir;

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
        AudioManager.instance.PlaySound("itemPick");
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
                splash.pick_up_animation(player.name, FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).name.Substring(5), pickUpSprite);

        Destroy(gameObject);
        return itemId;
    }
}
