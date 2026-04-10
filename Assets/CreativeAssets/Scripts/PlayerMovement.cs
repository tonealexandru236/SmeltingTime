using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canPlayerMove;

    [SerializeField] KeyCode upButton;
    [SerializeField] KeyCode downButton;
    [SerializeField] KeyCode rightButton;
    [SerializeField] KeyCode leftButton;

    [Header("Visual")]
    [SerializeField] SpriteRenderer playerSr;
    [SerializeField] Sprite[] dirSprites;

    [Header("Object")] 
    [SerializeField] Transform playerHand;

    Rigidbody2D rb;
    float d, td;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canPlayerMove = true;
    }
    private void Update()
    {
        if (!canPlayerMove) return;

        float yInput = (Input.GetKey(upButton) ? 1 : 0) + (Input.GetKey(downButton) ? -1 : 0);
        float xInput = (Input.GetKey(rightButton) ? 1 : 0) + (Input.GetKey(leftButton) ? -1 : 0);

        Vector2 dir = new Vector2(xInput, yInput).normalized;

        //Move Player
        float disPlayerMoves = Time.deltaTime * 4f;
        Vector2 nextPlayerPos = transform.position + (Vector3)dir * disPlayerMoves;

        LayerMask blockerMasks = LayerMask.GetMask("Station", "Wall");

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.35f, (nextPlayerPos - (Vector2)transform.position).normalized, disPlayerMoves, blockerMasks);

        if (hit.collider != null)
        {
            Vector2 hitPoint = hit.point;
            transform.position = (hitPoint + ((Vector2)transform.position - hitPoint).normalized * 0.35f);
        }
        else
        {
            transform.position = nextPlayerPos;
        }

        //transform.position += (Vector3)dir * Time.deltaTime * 4f;

        if (xInput == 0)
            td = yInput == 0 ? td : (yInput == 1 ? 0 : 4);
        else if (xInput == 1)
            td = yInput == 0 ? 2 : (yInput == 1 ? 1 : 3);
        else
            td = yInput == 0 ? 6 : (yInput == 1 ? 7 : 5);

        if (Mathf.Abs(td - d) <= 4f) d += Mathf.Sign(td - d) * Mathf.Min(Time.deltaTime * 25f, Mathf.Abs(td - d));
        else d = ((d -  Mathf.Sign(td - d) * Mathf.Min(25f * Time.deltaTime, 8 - Mathf.Abs(td - d))) + 8f) % 8f;


        //Change Sprite For Player
        int indx = Mathf.RoundToInt(d) % 8;
        if(indx < 5) {
            playerSr.sprite = dirSprites[indx];
            playerSr.transform.localScale = new Vector2(1, 1);
        }
        else {
            playerSr.sprite = dirSprites[8 - indx];
            playerSr.transform.localScale = new Vector2(-1, 1);
        }



        int q = Mathf.RoundToInt(d)%8;

        float x = (q > 0 && q < 4) ? 1 : (q > 4 ? -1 : 0);
        float y = (q < 2 || q > 6) ? 1 : (q > 2 && q < 6 ? -1 : 0);

        playerHand.localPosition = new Vector2(x, y).normalized * 0.5f;
        playerHand.GetComponent<PlayerHand>().SetOrientationForItemInHand(q);
    }
}
