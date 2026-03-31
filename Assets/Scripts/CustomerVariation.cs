using UnityEngine;
using UnityEngine.Rendering;

public class CustomerVariation : MonoBehaviour
{
    void Start()
    {

        float r = Random.Range(144, 228);
        float g = Random.Range(144, 228);
        float b = 512 - r - g;

        Color color = new Color(r / 255, g / 255, b / 255);

        GetComponent<SpriteRenderer>().color = color;
    }
}
