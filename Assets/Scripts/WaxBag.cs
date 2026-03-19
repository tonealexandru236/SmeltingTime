using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waxbag : MonoBehaviour
{
    public GameObject Wax;

    public void Start()
    {
        StartCoroutine(restock_wax());
    }

    public IEnumerator restock_wax()
    {
        while(true)
        {
            if(transform.childCount < 3)
            {
                for(int i=transform.childCount+1; i<=4; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(-0.05f, 0.02f), Random.Range(-0.12f, 0.12f), 0);

                    GameObject ist = Instantiate(Wax);
                    ist.transform.parent = gameObject.transform;
                    ist.transform.localPosition = pos;

                    ist.SetActive(true);

                    yield return new WaitForSeconds(0.2f);
                }    
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
