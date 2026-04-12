using UnityEngine;

public class BackgroundAnim : MonoBehaviour
{
    [SerializeField] Transform bg1;
    [SerializeField] Transform bg2;

    private void Update()
    {
        bg1.transform.localPosition = new Vector2(bg1.transform.localPosition.x, (bg1.transform.localPosition.y + Time.deltaTime * 30f) % 462.5f);
        bg2.transform.localPosition = new Vector2(bg2.transform.localPosition.x, (bg2.transform.localPosition.y - Time.deltaTime * 30f) % 462.5f);
    }
}
