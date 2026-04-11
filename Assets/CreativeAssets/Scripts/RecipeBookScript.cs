using UnityEngine;
using UnityEngine.UI;

public class RecipeBookScript : MonoBehaviour
{
    [SerializeField] Image mainRecipeImage;

    [Header("Categories")]
    [SerializeField] GameObject[] allTabs;

    [Header("RecipeSprites")]
    [SerializeField] Sprite[] pickaxeSprites;
    [SerializeField] Sprite[] swordSprites;
    [SerializeField] Sprite[] axeSprites;
    [SerializeField] Sprite[] chestplateSprites;
    [SerializeField] Sprite[] bootsSprites;
    [SerializeField] Sprite[] furnaceSprites;

    Sprite[] spritesForMainRecipe;

    string currentTab;
    float t;
    private void Start()
    {
        t = 0;
    }
    private void Update()
    {
        
        if(currentTab != "") {
            t += Time.deltaTime;
            t %= spritesForMainRecipe.Length;

            mainRecipeImage.sprite = spritesForMainRecipe[(int)t];
        }
        else
        {
            t = 0;
        }


        foreach(GameObject obj in allTabs)
        {
            if(currentTab == obj.name) {
                obj.transform.localPosition = Vector2.MoveTowards(obj.transform.localPosition, new Vector3(obj.transform.localPosition.x, 20, 0), Time.deltaTime * 200f);
            }
            else
            {
                obj.transform.localPosition = Vector2.MoveTowards(obj.transform.localPosition, new Vector3(obj.transform.localPosition.x, 0, 0), Time.deltaTime * 200f);
            }
        }
    }
    public void SelectTab(string n)
    {
        currentTab = n;

        if (n == "Pickaxe") spritesForMainRecipe = pickaxeSprites;
        if (n == "Sword") spritesForMainRecipe = swordSprites;
        if (n == "Axe") spritesForMainRecipe = axeSprites;
        if (n == "Chestplate") spritesForMainRecipe = chestplateSprites;
        if (n == "Boots") spritesForMainRecipe = bootsSprites;
        if (n == "Furnace") spritesForMainRecipe = furnaceSprites;
    }
}
