using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] PlayerHand player_hand_1;
    [SerializeField] PlayerHand player_hand_2;

    private void Update()
    {
        foreach (ItemScript itm in FindObjectsByType<ItemScript>(FindObjectsSortMode.None))
            itm.SetInPickUpRange(false);

        player_hand_1.ManualUpdate();
        player_hand_2.ManualUpdate();
    }
}
