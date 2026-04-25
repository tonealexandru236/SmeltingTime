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
    static public float trueMadness;

    public GameObject GameOverScreen;
    public TMP_Text GameOverText;

    public GameObject Retry;
    public GameObject Next;

    float t;
    int numOfCustomersServed;
    public bool IsEndless;

    float total_time;


    private void Start()
    {
        Time.timeScale = 1;

        if (IsEndless)
            customerServedText.fontSize = 42.5f;

        trueMadness = 1.5f;

        t = timerInMinutes * 60f;
        total_time = 0;

        numOfCustomersServed = 0;
    }
    private void Update()
    {
        //Debug.Log(trueMadness);
        if (t <= 0)
        {
            GameOverScreen.SetActive(true);
            Retry.SetActive(true);
            GameOverText.SetText("You Lost!");
            GameOverScreen.GetComponent<Animator>().Play("end-game");

            Time.timeScale = 0;
            return;
        }

        t -= Time.deltaTime;

        if (IsEndless)
        {
            total_time += Time.deltaTime;
            timerTxt.text = FloatToTime(total_time);
        }
        else
        {
            t -= Time.deltaTime;
            timerTxt.text = FloatToTime(t);
        }



        if (IsEndless)
            customerServedText.text = numOfCustomersServed.ToString();
        else
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

        if (IsEndless)
            trueMadness = Mathf.Max(1.1f, 1 + (50 - (float)numOfCustomersServed)/100);

        if (numOfCustomersServed == numOfCustomersToServe && IsEndless == false)
        {
            GameOverScreen.SetActive(true);
            if(Next != null) Next.SetActive(true);
            GameOverText.SetText("You Won!");
            GameOverScreen.GetComponent<Animator>().Play("end-game");

            Time.timeScale = 0;
        }
    }
}