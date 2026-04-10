using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().SetText("v" + Application.version);
    }
}
