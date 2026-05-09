using System.Collections;
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
            StartCoroutine(wait_for_key());
        }
    }

    IEnumerator wait_for_key()
    {
        if (gameObject.name == "F")
            gameObject.GetComponent<Animator>().Play("tactile-feedback", 0, 0);


        yield return new WaitForSecondsRealtime(0.01f);

        buttonTop.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        buttonTop.anchoredPosition = new Vector2(0, 12f);
    }
}
