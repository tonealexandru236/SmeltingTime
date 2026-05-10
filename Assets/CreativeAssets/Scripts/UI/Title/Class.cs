using UnityEngine;
using UnityEngine.UI;

public class Class : MonoBehaviour
{
    public GameObject class1;
    public GameObject class2;

    private void Awake()
    {
        if (PlayerPrefs.GetString("class-p1", "Craft") == "Smelt")
            switch_images();
    }

    public void switch_images()
    {
        Sprite img = null;
        img = class1.GetComponent<Image>().sprite;
        class1.GetComponent<Image>().sprite = class2.GetComponent<Image>().sprite;
        class2.GetComponent<Image>().sprite = img;
    }

    public void switch_class()
    {
        string cur1 = PlayerPrefs.GetString("class-p1", "Craft");
        string cur2 = PlayerPrefs.GetString("class-p2", "Smelt");

        PlayerPrefs.SetString("class-p1", cur2);
        PlayerPrefs.SetString("class-p2", cur1);

        switch_images();
    }
}
