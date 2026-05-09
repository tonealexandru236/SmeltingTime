using UnityEngine;
using UnityEngine.UI;

public class UiButtonFeedback : MonoBehaviour
{
    [SerializeField] KeyCode keyToBePressed;
    [SerializeField] RectTransform buttonTop;

    [SerializeField] bool hasIncrAnim;

    private void Update()
    {
        buttonTop.anchoredPosition = new Vector2(0, 20f);
        buttonTop.GetComponent<Image>().color = Color.white;
        if (Input.GetKey(keyToBePressed))
        {
            buttonTop.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            buttonTop.anchoredPosition = new Vector2(0, 12f);
        }
            
    }
}
