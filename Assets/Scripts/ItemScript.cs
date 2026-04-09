using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] string itemId;

    public Sprite[] itemSprites;

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
        foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
                splash.pick_up_animation(player.name, FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).name.Substring(5), pickUpSprite);

        Destroy(gameObject);
        return itemId;
    }
}
