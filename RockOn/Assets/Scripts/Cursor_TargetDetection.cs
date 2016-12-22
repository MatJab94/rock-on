using UnityEngine;

public class Cursor_TargetDetection : MonoBehaviour
{
    // target for the regular attack
    [HideInInspector]
    public GameObject target;

    void Start()
    {
        target = null;
    }

    // event that is called if target enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetForCursor")
        {
            target = collision.gameObject;
            // Debug.Log("enter");
        }
    }

    // event that is called if target exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetForCursor")
        {
            target = null;
            // Debug.Log("exit");
        }
    }
}
