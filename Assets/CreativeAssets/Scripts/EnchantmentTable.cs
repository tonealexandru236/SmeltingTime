using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnchantmentTable : MonoBehaviour
{
    [SerializeField] GameObject bookEnchant;

    [SerializeField] GameObject circleAnchor;
    [SerializeField] Image outerCircle;
    [SerializeField] TMP_Text innerText;
    [SerializeField] Gradient colorGradientOuterCircle;

    [Header("Visual")]
    [SerializeField] SpriteRenderer toEnchant;
    [SerializeField] SpriteRenderer finalEnchanted;

    string toEnchantId, finalEnchantedId;
    KeyCode currentPlayerKey;
    PlayerHand currentPlayerHand;

    float t, pU, spasmingRot;
    int finalEnchantmentLevel;
    private void Start()
    {
        t = 0;
        spasmingRot = 0;
        pU = -1;
        toEnchantId = "";
        finalEnchantedId = "";
    }
    private void Update()
    {
        #region Anim For Book
        bookEnchant.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        t += Time.deltaTime;
        t %= 1;

        bookEnchant.transform.localPosition = new Vector2(0, 0.4f + 0.2f * Mathf.Abs(t - 0.5f));
        #endregion

        if(pU >= 0)
        {
            pU += Time.deltaTime;
            spasmingRot = (spasmingRot + Time.deltaTime * 4f) % 1;
            pU = Mathf.Min(pU, 3);

            if(pU > 0.1f)
            {
                if (toEnchantId == "")
                {
                    pU = -1;
                    return;
                }
                    

                outerCircle.fillAmount = (pU % 1);
                if(pU == 3)
                    outerCircle.fillAmount = 1;

                if (pU > 1)
                    innerText.text = ((int)pU).ToString();

                circleAnchor.transform.localScale = new Vector2(1, 1) * (0.5f + 0.5f * pU / 3f);

                circleAnchor.transform.localEulerAngles = new Vector3(0, 0, -5f + Mathf.Abs(spasmingRot - 0.5f) * 10f);

                outerCircle.color = colorGradientOuterCircle.Evaluate(pU / 3);

                if (!Input.GetKey(currentPlayerKey))
                {
                    if (pU <= 0.1f)
                    {
                        if(finalEnchantedId != "")
                        {
                            currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById(finalEnchantedId).itemSprites, finalEnchantedId, finalEnchantmentLevel);
                            finalEnchanted.sprite = null;
                            finalEnchantedId = "";
                        }
                        else
                        {
                            currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById(toEnchantId).itemSprites, toEnchantId, 0);
                            toEnchant.sprite = null;
                            toEnchantId = "";
                        }
                        finalEnchantmentLevel = 0;

                    }
                    else if(pU > 1)
                    {
                        finalEnchantmentLevel = (int)pU;
                        finalEnchanted.sprite = toEnchant.sprite;
                        finalEnchantedId = toEnchantId;

                        toEnchant.sprite = null;
                        toEnchantId = "";
                    }

                    pU = -1;

                    outerCircle.fillAmount = 0;
                    innerText.text = "";
                    circleAnchor.transform.localScale = new Vector2(0.5f, 0.5f);
                    currentPlayerHand.transform.parent.GetComponent<PlayerMovement>().canPlayerMove = true;
                }
            }
        }
    }

    public void UseEnchantmentTable(string itemId, PlayerHand ph, KeyCode k)
    {
        if (itemId != "") {
            toEnchantId = itemId;
            toEnchant.sprite = FindFirstObjectByType<ItemDatabase>().GetObjById(itemId).itemSprites[0];
            ph.RemoveItemInHand();
        }
        else if(toEnchantId != "" || finalEnchantedId != "")
        {
            if(finalEnchantedId != "")
            {
                currentPlayerHand.PickUpItemInHand(FindFirstObjectByType<ItemDatabase>().GetObjById(finalEnchantedId).itemSprites, finalEnchantedId, finalEnchantmentLevel);
                finalEnchanted.sprite = null;
                finalEnchantedId = "";
                return;
            }

            ph.transform.parent.GetComponent<PlayerMovement>().canPlayerMove = false;

            pU = 0;
            currentPlayerKey = k;
            currentPlayerHand = ph;
        }
    }
}
