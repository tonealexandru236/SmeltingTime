using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] PlayerHand player_hand_1;
    [SerializeField] PlayerHand player_hand_2;

    List<SpriteRenderer> dynamicSprites = new List<SpriteRenderer>();

    private void Start()
    {
        foreach (StationScript station in FindObjectsByType<StationScript>(FindObjectsSortMode.None))
            dynamicSprites.Add(station.GetComponent<SpriteRenderer>());

        dynamicSprites.Add(player_hand_1.transform.parent.GetComponent<PlayerMovement>().playerSr);
        dynamicSprites.Add(player_hand_2.transform.parent.GetComponent<PlayerMovement>().playerSr);
    }
    private void Update()
    {
        foreach (ItemScript itm in FindObjectsByType<ItemScript>(FindObjectsSortMode.None))
            itm.SetInPickUpRange(false);

        player_hand_1.ManualUpdate();
        player_hand_2.ManualUpdate();




        foreach (SpriteRenderer q in dynamicSprites)
            q.sortingOrder = (int)(1000 - (q.transform.position.y + 50) * 10);
    }
}
