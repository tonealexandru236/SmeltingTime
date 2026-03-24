using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerClickHandler
{
    private float InitSize;
    public float multiplier;
    private float deactivate_multiplier;
    public float Time;

    public void Start()
    {
        InitSize = gameObject.transform.localScale.x;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if(GetComponent<TMP_Text>()) GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;

        transform.DOScaleX(InitSize * multiplier, Time).SetUpdate(true);
        transform.DOScaleY(InitSize * multiplier, Time).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (GetComponent<TMP_Text>()) GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;

        transform.DOScaleX(InitSize, Time).SetUpdate(true);
        transform.DOScaleY(InitSize, Time).SetUpdate(true);
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.transform.DOScaleX(InitSize * 0.9f, Time/2);
        gameObject.transform.DOScaleY(InitSize * 0.9f, Time/2);
    }*/
}
