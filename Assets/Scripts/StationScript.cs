using UnityEngine;

public class StationScript : MonoBehaviour
{
    public string stationTag;

    [SerializeField] GameObject arrowVisual;

    bool closeInRange;
    void Start() {
        closeInRange = false;
    }
    void Update() {
        arrowVisual.SetActive(closeInRange);
    }
    public void UseStation(PlayerHand ph) {
        if(stationTag == "trash") {
            ph.RemoveItemInHand();
        }
        else if (stationTag == "crafting") {
            GetComponent<CraftingTable>().UseCrafting(ph.itemInHandID, ph, KeyCode.F);
        }
    }
    public void ActivateStation(bool actv, PlayerHand ph) {
        closeInRange = false;
        if(actv)
            closeInRange = actv;
    }
}
