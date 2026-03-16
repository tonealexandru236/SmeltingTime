using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] Transform conveyorVisual;

    [Header("Spawn Logic")]
    [SerializeField] GameObject itemToSpawn;
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

            GameObject newItem = Instantiate(itemToSpawn, spawnPoint.transform.position, Quaternion.identity);
            newItem.transform.parent = itemsParent;
        }
    }
}
