using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] Transform conveyorVisual;

    [Header("Items")]
    [SerializeField] GameObject[] itemsToSpawn;
    [SerializeField] int[] itemsChance;


    [Header("Spawn Logic")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform itemsParent;
    [SerializeField] float spawnRate; // seconds between spawn

    float t;
    void Update()
    {
        //Move Anim
        conveyorVisual.position =  new Vector3(conveyorVisual.position.x, (conveyorVisual.position.y - Time.deltaTime * 1f) % 3, 0);
        itemsParent.position -= new Vector3(0, Time.deltaTime, 0);

        //Spawn Logic
        t += Time.deltaTime;
        if(t >= spawnRate) {
            t %= spawnRate;

            GameObject newItem = Instantiate(random_item(), spawnPoint.transform.position, Quaternion.identity);
            newItem.transform.position = new Vector3(newItem.transform.position.x, newItem.transform.position.y, -1); /// layering bug fix
            newItem.transform.parent = itemsParent;
        }
    }

    private int cumulative_chance;
    public GameObject random_item()
    {
        int chance = Random.Range(1, 101);
        cumulative_chance = 0;
        Debug.Log(chance);
        for (int i=0; i<itemsToSpawn.Length; i++)
        {
            //Debug.Log(" " + cumulative_chance);
            cumulative_chance += itemsChance[i];
            if (chance <= cumulative_chance)
                return itemsToSpawn[i]; 
        }
        return null;
    }

}
