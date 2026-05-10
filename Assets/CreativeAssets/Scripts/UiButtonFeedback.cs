using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonFeedback : MonoBehaviour
{
    [SerializeField] CanvasGroup ButtonCg;

    [SerializeField] KeyCode keyToBePressed;
    [SerializeField] RectTransform buttonTop;

    [SerializeField] bool hasIncrAnim;

    float t;
    private void Update()
    {
        buttonTop.GetComponent<Image>().color = Color.white;
        buttonTop.anchoredPosition = new Vector2(0, 20f);
        if (Input.GetKey(keyToBePressed))
        {
            buttonTop.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            buttonTop.anchoredPosition = new Vector2(0, 12f);

            t = 1;
        }

        if(ButtonCg)
        {
            t -= Time.deltaTime * 2f;
            t = Mathf.Max(t, 0);

            ButtonCg.alpha = t;
        }
    }
}
