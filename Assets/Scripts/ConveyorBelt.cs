using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    void Update()
    {
        transform.position =  new Vector3(transform.position.x, (transform.position.y - Time.deltaTime * 1f) % 3, 0);
    }
}
