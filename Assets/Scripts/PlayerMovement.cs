using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] KeyCode upButton;
    [SerializeField] KeyCode downButton;
    [SerializeField] KeyCode rightButton;
    [SerializeField] KeyCode leftButton;

    [Header("Visual")]
    [SerializeField] SpriteRenderer playerSr;
    [SerializeField] Sprite[] dirSprites;

    [Header("Object")] // TO REMOVE
    [SerializeField] SpriteRenderer objHeld;
    [SerializeField] Sprite[] log4dir;

    Rigidbody2D rb;
    float d, td;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float yInput = (Input.GetKey(upButton) ? 1 : 0) + (Input.GetKey(downButton) ? -1 : 0);
        float xInput = (Input.GetKey(rightButton) ? 1 : 0) + (Input.GetKey(leftButton) ? -1 : 0);

        Vector2 dir = new Vector2(xInput, yInput).normalized;

        transform.position += (Vector3)dir * Time.deltaTime * 4f;

        if (xInput == 0)
            td = yInput == 0 ? td : (yInput == 1 ? 0 : 4);
        else if (xInput == 1)
            td = yInput == 0 ? 2 : (yInput == 1 ? 1 : 3);
        else
            td = yInput == 0 ? 6 : (yInput == 1 ? 7 : 5);

        if (Mathf.Abs(td - d) <= 4f) d += Mathf.Sign(td - d) * Mathf.Min(Time.deltaTime * 20f, Mathf.Abs(td - d));
        else d = ((d -  Mathf.Sign(td - d) * Mathf.Min(20f * Time.deltaTime, 8 - Mathf.Abs(td - d))) + 8f) % 8f;

        playerSr.sprite = dirSprites[Mathf.RoundToInt(d) % 8];

        int q = Mathf.RoundToInt(d);

        float x = (q > 0 && q < 4) ? 1 : (q > 4 ? -1 : 0);
        float y = (q < 2 || q > 6) ? 1 : (q > 2 && q < 6 ? -1 : 0);

        objHeld.transform.localPosition = new Vector2(x, y).normalized;
        objHeld.sprite = q % 4 == 0 ? log4dir[1] : (q % 2 == 0 ? log4dir[0] : (q == 1 || q == 5 ? log4dir[3] : log4dir[2]));
    }
}
