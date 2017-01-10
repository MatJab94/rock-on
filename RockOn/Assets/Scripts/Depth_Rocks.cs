using UnityEngine;

public class Depth_Rocks : MonoBehaviour
{
    void Start()
    {
        // change the Z value based on Y, only once for non-moving objects like Rocks
        Vector3 depth = gameObject.transform.position;
        depth.z = depth.y;
        gameObject.transform.position = depth;
    }
}
