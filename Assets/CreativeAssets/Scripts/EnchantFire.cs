using UnityEngine;

public class EnchantFire : MonoBehaviour
{
    [SerializeField] Color[] enchantColors;
    [SerializeField] Sprite[] fireAnim;

    float t;
    private void Start()
    {
        t = 0;
    }
    private void Update()
    {
        if (transform.parent != null)
        {
            transform.localEulerAngles = new Vector3(0, 0, 360) - transform.parent.localEulerAngles;
            transform.localScale = transform.parent.localScale;
        }
            

        t += Time.deltaTime;
        t %= 1;

        GetComponent<SpriteRenderer>().sprite = fireAnim[(int)(t * fireAnim.Length)];
    }
    public void SetUpFire(int lvl)
    {
        GetComponent<SpriteRenderer>().color = enchantColors[lvl];
    }
}
