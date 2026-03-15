using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Sprite[] itemSprites;

    [Header("Visual")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite pickUpSprite;

    public void SetInPickUpRange(bool canBePickedUp)
    {
        sr.sprite = canBePickedUp ? pickUpSprite : normalSprite;
    }
    public void PickUpItem()
    {
        Destroy(gameObject);
    }
}
