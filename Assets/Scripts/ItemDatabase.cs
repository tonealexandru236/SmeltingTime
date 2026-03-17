using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] string[] itemID;
    [SerializeField] ItemScript[] itemAsObj;

    public ItemScript GetObjById(string id)
    {
        for (int i = 0; i < itemID.Length; i++)
            if (itemID[i] == id)
                return itemAsObj[i];
        return null;
    }
}
