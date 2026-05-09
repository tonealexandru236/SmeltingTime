using UnityEngine;

public class EnchantmentTable : MonoBehaviour
{
    [SerializeField] GameObject bookEnchant;

    float t;
    private void Start()
    {
        t = 0;
        
    }
    private void Update()
    {
        bookEnchant.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        t += Time.deltaTime;
        t %= 1;

        bookEnchant.transform.localPosition = new Vector2(0, 0.4f + 0.2f * Mathf.Abs(t - 0.5f));
    }

    private GameObject player;

    public void UseEnchantmentTable(string itemId, PlayerHand ph, KeyCode k)
    {
        player = ph.player;
        if (itemId != "") {
            ph.RemoveItemInHand();
        }
    }
}
