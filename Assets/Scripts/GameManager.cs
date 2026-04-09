using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timerInMinutes;
    [SerializeField] int numOfCustomersToServe;
    [SerializeField] TMP_Text timerTxt;
    [SerializeField] TMP_Text customerServedText;

    float t;
    int numOfCustomersServed;
    private void Start()
    {
        t = timerInMinutes * 60f;
        numOfCustomersServed = 0;
    }
    private void Update()
    {
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
    }
}
