using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomersOrderLine : MonoBehaviour
{
    [SerializeField] float secondsBeforeMad;

    [SerializeField] GameObject customerPref;
    [SerializeField] int numberOfCustomers;
    [SerializeField] float spaceBetweenThem;

    [Header("UI")]
    [SerializeField] Sprite checkSprite;
    [SerializeField] Sprite xSprite;
    [SerializeField] ItemScript[] itemsItCanOrder;
    [SerializeField] SpriteRenderer itemVisual;
    [SerializeField] GameObject checkMark;
    [SerializeField] Transform menuParent;

    [Header("Emoji")]
    [SerializeField] SpriteRenderer emojiSr;
    [SerializeField] Sprite[] emojiSprites;

    [Header("Customer Visual")]
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite downSprite;

    List<GameObject> customerLine = new List<GameObject>();

    ItemScript itemOrder;

    float delayBetween, yState, targetSpriteAlpha, s = 0;
    private void Update()
    {
        s += Time.deltaTime;
        if(s > secondsBeforeMad && s < 1000)
        {
            checkMark.GetComponent<SpriteRenderer>().sprite = xSprite;
            StartCoroutine(MakeCustomerLeave());
        }
        else if (s < 1000)
        {
            emojiSr.sprite = emojiSprites[(int)(Mathf.Min(s / secondsBeforeMad, .999f) * emojiSprites.Length)];
        }


        if (customerLine.Count > 0 && itemOrder == null && customerLine[0].transform.position == transform.position && !checkMark.activeSelf)
        {
            itemOrder = itemsItCanOrder[Random.Range(0, itemsItCanOrder.Length)];
            itemVisual.sprite = itemOrder.itemSprites[0];
            targetSpriteAlpha = 1;
            yState = 1;
            s = 0;
        }

        if (customerLine.Count < numberOfCustomers)
        {
            if (delayBetween <= 0)
            {
                AddNewCustomerInLine();

                delayBetween = .5f;
            }
            delayBetween -= Time.deltaTime;
        }

        if (menuParent.localScale.y != yState)
            menuParent.localScale = new Vector3(1, Mathf.MoveTowards(menuParent.localScale.y, yState, Time.deltaTime * 5f));
        if (itemVisual.color.a != targetSpriteAlpha)
            itemVisual.color = new Color(itemVisual.color.r, itemVisual.color.g, itemVisual.color.b, Mathf.MoveTowards(itemVisual.color.a, targetSpriteAlpha, Time.deltaTime * 5f));

        MoveCustomers();
    }
    private void AddNewCustomerInLine()
    {
        GameObject newCustomer = Instantiate(customerPref, transform.position + new Vector3(spaceBetweenThem * numberOfCustomers, 0, 0), Quaternion.identity);
        customerLine.Add(newCustomer);
        newCustomer.transform.parent = transform;
    }
    private void MoveCustomers()
    {
        for (int i = 0; i < customerLine.Count; i++)
        {
            Vector2 targetPos = transform.position + new Vector3(i * spaceBetweenThem, 0, 0);
            customerLine[i].transform.position = Vector2.MoveTowards(customerLine[i].transform.position, targetPos, Time.deltaTime * 3f);
            //customerLine[i].GetComponent<SpriteRenderer>().sortingOrder = i + 10;
        }
    }
    IEnumerator MakeCustomerLeave()
    {
        s = 2000;

        checkMark.SetActive(true);
        targetSpriteAlpha = 0;
        itemOrder = null;

        yield return new WaitForSeconds(0.07f);
        checkMark.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.07f);
        checkMark.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.07f);
        checkMark.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.07f);
        checkMark.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(0.2f);

        yState = 0;
        while (yState != menuParent.localScale.y)
            yield return new WaitForEndOfFrame();

        checkMark.SetActive(false);

        GameObject customer = customerLine[0];
        customerLine.RemoveAt(0);

        customer.GetComponent<SpriteRenderer>().sprite = downSprite;

        Vector2 targetLateralPos = transform.position - new Vector3(0, spaceBetweenThem, 0);
        while((Vector2)customer.transform.position != targetLateralPos)
        {
            customer.transform.position = Vector2.MoveTowards(customer.transform.position, targetLateralPos, Time.deltaTime * 3f);
            yield return new WaitForEndOfFrame();
        }

        customer.GetComponent<SpriteRenderer>().sprite = rightSprite;
        customer.GetComponent<SpriteRenderer>().sortingOrder += 1;

        Vector2 targetEndPos = transform.position - new Vector3(-spaceBetweenThem * numberOfCustomers, spaceBetweenThem, 0);
        while ((Vector2)customer.transform.position != targetEndPos)
        {
            customer.transform.position = Vector2.MoveTowards(customer.transform.position, targetEndPos, Time.deltaTime * 3f);
            yield return new WaitForEndOfFrame();
        }
    }
    public void ActivateCustomer(ItemScript itm, PlayerHand ph) //Gen daca sa gen da
    {
        if (itemOrder == null) return;

        if(ph.playerPriority == 0)
            itemVisual.sprite = itemOrder.itemSprites[0];
        if(itm != null && itm == itemOrder)
            itemVisual.sprite = itemOrder.pickUpSprite;
    }

    private float add;
    public TMP_Text score_text;

    public void GiveItemtoCustomer(ItemScript itm, PlayerHand ph)
    {
        if (itemOrder == null || itm == null || itm != itemOrder)
            return;

        #region Score Stuff
        add = 1;
        float score = PlayerPrefs.GetFloat("Score");
        score += add;
        PlayerPrefs.SetFloat("Score", score);

        score_text.SetText("score " + score.ToString());

        #endregion

        FindFirstObjectByType<GameManager>().ServedOneCustomer();
        
        ph.RemoveItemInHand();

        checkMark.GetComponent<SpriteRenderer>().sprite = checkSprite;
        StartCoroutine(MakeCustomerLeave());
    }
}
