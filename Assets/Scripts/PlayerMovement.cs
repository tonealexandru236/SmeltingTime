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
            playerSr.sprite = yInput == 1 ? dirSprites[0] : ((yInput == -1) ? dirSprites[2] : playerSr.sprite);
        else if (xInput == 1)
            playerSr.sprite = (yInput == 1) ? dirSprites[4] : (yInput == -1 ? dirSprites[5] : dirSprites[1]);
        else
            playerSr.sprite = (yInput == 1) ? dirSprites[7] : (yInput == -1 ? dirSprites[6] : dirSprites[3]);

        //Obj
        if (xInput == yInput && xInput == 0) return;

        objHeld.transform.localPosition = new Vector2(xInput, yInput).normalized;
        if (xInput != 0 && yInput == 0) objHeld.sprite = log4dir[0];
        else if(xInput == 0 && yInput != 0) objHeld.sprite = log4dir[1];
        else if(xInput == yInput) objHeld.sprite = log4dir[3];
        else  objHeld.sprite = log4dir[2];
    }
}
