using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timerInMinutes;
    [SerializeField] int numOfCustomersToServe;
    [SerializeField] TMP_Text timerTxt;
    [SerializeField] TMP_Text customerServedText;

    public GameObject GameOverScreen;
    public TMP_Text GameOverText;

    public GameObject Retry;
    public GameObject Next;

    float t;
    int numOfCustomersServed;
    private void Start()
    {
        Time.timeScale = 1;
        t = timerInMinutes * 60f;
        numOfCustomersServed = 0;
    }
    private void Update()
    {
        if(t <= 0)
        {
            GameOverScreen.SetActive(true);
            Retry.SetActive(true);
            GameOverText.SetText("You Lost!");
            GameOverScreen.GetComponent<Animator>().Play("end-game");

            Time.timeScale = 0;
            return;
        }

        t -= Time.deltaTime;

        timerTxt.text = FloatToTime(t);
        customerServedText.text = numOfCustomersServed.ToString() + "/" + numOfCustomersToServe.ToString();
    }
    private string FloatToTime(float a)
    {
        int m = (int)(a / 60);
        int s = (int)(a % 60);

        string r = "";

        if (m < 10) r += "0";
        r += m.ToString();

        r += ":";

        if (s < 10) r += "0";
        r += s.ToString();

        return r;
    }
    public void ServedOneCustomer()
    {
        numOfCustomersServed += 1;

        if (numOfCustomersServed == numOfCustomersToServe)
        {
            GameOverScreen.SetActive(true);
            Next.SetActive(true);
            GameOverText.SetText("You Won!");
            GameOverScreen.GetComponent<Animator>().Play("end-game");

            Time.timeScale = 0;
        }
    }
}