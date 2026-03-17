using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] string[] recipes;

    [Header("Visual")]
    [SerializeField] GameObject[] recipeItemsVisual; //Max 4
    [SerializeField] Transform leftEnd;
    [SerializeField] Transform rightEnd;
    [SerializeField] Transform midTube;
    [SerializeField] GameObject resultParent;
    [SerializeField] SpriteRenderer itemToCraftSpriteRenderer;
    [SerializeField] Image holdToCraftImage;

    [SerializeField] List<string> itemsInCraft = new List<string>();

    KeyCode useKey; PlayerHand lastPh; ItemDatabase itemDb;
    ItemScript craftedItem; string craftedString;
    float t;
    void Start() {
        t = -1;
        itemDb = FindFirstObjectByType<ItemDatabase>();
    }
    void Update() {
        if(t >= 0) {
            lastPh.pm.canPlayerMove = false;
            t += Time.deltaTime;
            if(t >= 0.2f) {
                holdToCraftImage.fillAmount = t;
            }
            if(t >= 1f && craftedItem != null) {
                //Craft
                lastPh.PickUpItemInHand(craftedItem.itemSprites, craftedString);
                itemsInCraft.Clear();
                RefreshRecipePart();
            }

            if (Input.GetKeyUp(useKey)) {
                lastPh.pm.canPlayerMove = true;
                if (t <= 0.2f) {
                    //Remove Last Item
                    string n = itemsInCraft[itemsInCraft.Count - 1];
                    ItemScript isForLast = null;

                    isForLast = itemDb.GetObjById(n);

                    lastPh.PickUpItemInHand(isForLast.itemSprites, n);
                    itemsInCraft.RemoveAt(itemsInCraft.Count - 1);
                    RefreshRecipePart();
                }
                t = -1;
            }
        }
        else {
            holdToCraftImage.fillAmount = 0;
        }
    }

    private GameObject player;

    public void UseCrafting(string itemId, PlayerHand ph, KeyCode kc) {
        player = ph.player;
        if (itemId != "") {
            itemsInCraft.Add(itemId);
            RefreshRecipePart();
            ph.RemoveItemInHand();
        }
        else if (itemsInCraft.Count != 0 && t < 0){
            useKey = kc;
            lastPh = ph;
            t = 0;
        }
    }

    private void RefreshRecipePart() {
        //Set Ui
        leftEnd.localPosition = new Vector2(-.125f - 0.25f * (Mathf.Max(0, itemsInCraft.Count - 1)), leftEnd.localPosition.y);
        rightEnd.localPosition = new Vector2(.125f + 0.25f * (Mathf.Max(0, itemsInCraft.Count - 1)), leftEnd.localPosition.y);
        midTube.transform.localScale = new Vector2(Mathf.Max(0, itemsInCraft.Count - 1), 1);

        //Set Items
        for(int i = 0; i < 5; i++)
            recipeItemsVisual[i].SetActive(false);
        
        float startX = (itemsInCraft.Count - 1) * -0.25f;
        for(int i = 0; i < itemsInCraft.Count; i++) {

            recipeItemsVisual[i].GetComponent<SpriteRenderer>().sprite = itemDb.GetObjById(itemsInCraft[i]).itemSprites[0];
            
            recipeItemsVisual[i].transform.localPosition = new Vector2(startX, 0);
            recipeItemsVisual[i].SetActive(true);
            startX += .5f;
        }

        //Set Final Product
        string[] itemsInCraftArray = itemsInCraft.ToArray();
        Array.Sort(itemsInCraftArray);

        string craftString = "";
        for(int i = 0; i < itemsInCraftArray.Length; i++) {
            craftString += itemsInCraftArray[i];
            if(i != itemsInCraftArray.Length - 1) craftString += ",";
        }

        resultParent.SetActive(false);
        craftedItem = null;
        for(int i = 0; i < recipes.Length; i++) {
            string craftName = "", recipeString = "";
            bool addToCraftName = true;
            for(int j = 0; j < recipes[i].Length; j++) {
                if(recipes[i][j] == '-') {
                    addToCraftName = false;
                }
                else {
                    if(addToCraftName) craftName += recipes[i][j];
                    else recipeString += recipes[i][j];
                }
            }

            if(recipeString == craftString) {
                //Craft Match
                ItemScript itm = itemDb.GetObjById(craftName);

                itemToCraftSpriteRenderer.sprite = itm.itemSprites[0];
                craftedItem = itm;
                craftedString = craftName;

                resultParent.SetActive(true);
                break;
            }
        }
    }
}
