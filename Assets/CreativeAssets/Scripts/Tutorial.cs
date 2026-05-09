using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TMP_Text text;
    public GameObject[] tutorialSpeech;
    private float check;

    private void Awake()
    {
        check = 1;
    }

    void Update()
    {
        if (check == 1)
        {
            text.SetText("Welcome to                   ! \n\n You and your friend are operating a factory (a surprisingly small one) and your job is to manufacture items and sell them to people! \n\n Your boss is watching you, so don't slack off or you'll both lose! Finish the customer goal to win the game. \n\n Each Player has a different job! You can switch between these jobs on the Title Screen. More jobs, items and game mechanics get unlocked the further you advance. \n\n Cooperate to win!");
            Time.timeScale = 0f;
        }
    }

    public void understood()
    {
        check = 0;
        Time.timeScale = 1f;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

        StartCoroutine(speech());
    }

    IEnumerator speech()
    {
        Time.timeScale = 1f;

        yield return new WaitForSeconds(3.8f);
        for (int i=0; i<tutorialSpeech.Length; i++)
        {
            tutorialSpeech[i].SetActive(true);
            yield return new WaitForSeconds(3.75f);
        }
    }
}
