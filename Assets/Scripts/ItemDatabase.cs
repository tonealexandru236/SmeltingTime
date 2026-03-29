using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    List<string> itemID = new List<string>();
    [SerializeField] ItemScript[] itemAsObj;

    private void Start()
    {
        itemID.Clear();
        foreach (ItemScript iscr in itemAsObj)
        {
            bool a = false;
            string nm = "";
            for(int i = 0; i < iscr.name.Length; i++)
            {
                if (iscr.name[i] == '-') a = true;
                else if (a && iscr.name[i] != ' ') nm += iscr.name[i];
            }
            itemID.Add(nm.ToLower());
        }
    }
    public ItemScript GetObjById(string id)
    {
        for (int i = 0; i < itemID.Count; i++)
            if (itemID[i] == id)
                return itemAsObj[i];
        return null;
    }
}
