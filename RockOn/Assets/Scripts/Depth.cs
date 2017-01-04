using UnityEngine;

/* 
 * Simple script for adding "depth" to our 2D world.
 * Just add this script to the GameObject and voila.
 * 
 * The script takes the GameObject's position and
 * changes it so that the position on Z axis is
 * the same as Y axis.
 */
public class Depth : MonoBehaviour
{

    // this GameObject's Transform component
    private Transform _transform;

    // Vector storing the position
    private Vector3 _depth;

    void Start()
    {
        // initializing the variable with this GameObject's Transform
        _transform = GetComponent<Transform>();

        _depth = _transform.position;
    }

    // movement already happens in FixedUpdate, so changing the depth can also be in FixedUpdate
    void FixedUpdate()
    {
        // update the Z position to be equal the Y position
        _depth.x = _transform.position.x;
        _depth.y = _transform.position.y;
        _depth.z = _transform.position.y;
        _transform.position = _depth;
    }
}
