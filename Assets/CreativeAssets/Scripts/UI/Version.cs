using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("s_version", 1) == 0)
            gameObject.GetComponent<TMP_Text>().SetText("");
        else
            gameObject.GetComponent<TMP_Text>().SetText("v" + Application.version);
    }
}
