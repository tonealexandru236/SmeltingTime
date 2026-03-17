using System.Collections;
using UnityEngine;

public class StationScript : MonoBehaviour
{
    public string stationTag;
    public GameObject throwable_item;

    [SerializeField] GameObject arrowVisual;

    bool closeInRange;
    void Start() {
        closeInRange = false;
    }
    void Update() {
        arrowVisual.SetActive(closeInRange);
    }
    public void UseStation(PlayerHand ph) {
        if (ph.itemInHandID != "")
        {
            foreach (ItemSplashes splash in FindObjectsByType<ItemSplashes>(FindObjectsSortMode.None))
                if (ph.player != null)
                    splash.pick_down_animation(ph.player.name, ph.itemInHandID, ph.itemInHandSprites[1]);
        }

        if (stationTag == "trash" && ph.itemInHandID != "")
        {
            StartCoroutine(Item_fly_animation(ph.transform.position, 0.75f, 1.1f, ph.itemInHandSprites[1]));
            ph.RemoveItemInHand();
        }
        else if (stationTag == "crafting")
        {
            GetComponent<CraftingTable>().UseCrafting(ph.itemInHandID, ph, KeyCode.F);
        }
    }
    public void ActivateStation(bool actv, PlayerHand ph) {
        closeInRange = false;
        if(actv)
            closeInRange = actv;
    }


    public IEnumerator Item_fly_animation(Vector3 from_pos, float duration, float arc_height, Sprite sprite)
    {
        GameObject ist = Instantiate(throwable_item);

        ist.GetComponent<SpriteRenderer>().sprite = sprite;
        ist.transform.position = gameObject.transform.position;
        ist.SetActive(true);

        arc_height = (transform.position.y - from_pos.y) + arc_height;

        Vector3 start_pos = from_pos;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            Vector3 cur_pos = Vector3.Lerp(start_pos, gameObject.transform.position, t);

            cur_pos.y += arc_height * t * (1 - t) * 4;
            cur_pos.z = -1;

            ist.transform.position = cur_pos;

            yield return null;
        }

        Destroy(ist);
    }
}
