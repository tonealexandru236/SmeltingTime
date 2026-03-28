using UnityEngine;

public class FurnaceScript : MonoBehaviour
{
    [SerializeField] string[] furnaceRecipes;

    [Header("Visual")]
    [SerializeField] ParticleSystem smokeParticles;
    [SerializeField] SpriteRenderer fuelImg;
    [SerializeField] SpriteRenderer toBurnImg;
    [SerializeField] SpriteRenderer resultImg;
    [SerializeField] Sprite normalBg;
    [SerializeField] Sprite fireBg;
    [SerializeField] Sprite[] woodBurningAnim;
    [SerializeField] Sprite[] charcoalBurningAnim;

    PlayerHand lastPh;
    ItemDatabase itemDb;
    string fuelUsed, whatToBurn, result;
    float t = 0;
    private void Start()
    {
        itemDb = FindFirstObjectByType<ItemDatabase>();
        fuelUsed = ""; whatToBurn = ""; result = "";
    }
    private void Update()
    {
        if(t > 0)
        {
            if(!smokeParticles.isPlaying)
                smokeParticles.Play();
            t -= Time.deltaTime;

            if(t <= 0)
            {
                toBurnImg.sprite = null;
                fuelImg.sprite = normalBg;
                smokeParticles.Stop();

                resultImg.sprite = itemDb.GetObjById(result).itemSprites[1];
            }
            else
            {
                if (result == "charcoal")
                    fuelImg.sprite = woodBurningAnim[Mathf.FloorToInt(((3 - t) / 3f) * woodBurningAnim.Length)];
                else
                    fuelImg.sprite = charcoalBurningAnim[Mathf.FloorToInt(((3 - t) / 3f) * charcoalBurningAnim.Length)];
            }
        }
    }
    public void UseFurnace(string itemId, PlayerHand ph)
    {
        if (t > 0) return;


        if (ph.itemInHandID == "")
        {
            if (result != "")
            {
                ph.PickUpItemInHand(itemDb.GetObjById(result).itemSprites, result);
                result = "";
                resultImg.sprite = null;
            }
            else
            {
                if(whatToBurn != "")
                {
                    ph.PickUpItemInHand(itemDb.GetObjById(whatToBurn).itemSprites, whatToBurn);
                    whatToBurn = "";
                    toBurnImg.sprite = null;
                }
                else if(fuelUsed != "")
                {
                    ph.PickUpItemInHand(itemDb.GetObjById(fuelUsed).itemSprites, fuelUsed);
                    fuelUsed = "";
                    fuelImg.sprite = normalBg;
                }
            }
        }

        if (itemId != "")
        {
            #region Puts Player Item In The Furnace
            //Tries To Fill Fuel
            if (fuelUsed == "" && (itemId == "log" || itemId == "charcoal"))
            {
                fuelUsed = itemId;
                fuelImg.sprite = itemDb.GetObjById(itemId).itemSprites[1];
            }
            //Fill To_Burn
            else
            {
                if (whatToBurn != "") return;

                whatToBurn = itemId;
                toBurnImg.sprite = itemDb.GetObjById(whatToBurn).itemSprites[1];
            }
            ph.RemoveItemInHand();
            #endregion

            //Calculate 
            if(fuelUsed == "log" && whatToBurn == "log")
            {
                result = "charcoal";

                //Clear Furnace
                fuelUsed = "";
                whatToBurn = "";
                fuelImg.sprite = fireBg;

                t = 3;
            }
            if(fuelUsed == "charcoal" && whatToBurn != "")
            {
                for(int i = 0; i < furnaceRecipes.Length; i++)
                {
                    bool addToRaw = true;
                    string raw = "", prod = "";
                    for(int j = 0; j < furnaceRecipes[i].Length; j++)
                    {
                        if (furnaceRecipes[i][j] == '-') addToRaw = false;
                        else if (addToRaw) raw += furnaceRecipes[i][j].ToString();
                        else prod += furnaceRecipes[i][j].ToString();
                    }

                    if(raw == whatToBurn)
                    {
                        result = prod;

                        //Clear Furnace
                        fuelUsed = "";
                        whatToBurn = "";
                        fuelImg.sprite = fireBg;

                        t = 3;
                    }
                }
            }
        }
    }
}
