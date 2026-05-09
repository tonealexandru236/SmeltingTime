using UnityEngine;

public class ScriptAnim : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] animSprites;

    [SerializeField] float fps;

    float t;
    private void Start()
    {
        t = 0;
    }

    private void Update()
    {
        t += fps * Time.deltaTime;
        t %= animSprites.Length;

        sr.sprite = animSprites[(int)t];
    }
}
