using UnityEngine;

public class Cursor_TargetDetection : MonoBehaviour
{
    // target for the regular attack
    [HideInInspector]
    public GameObject _target;

    void Start()
    {
        _target = null;
    }

    public GameObject getTarget()
    {
        return _target;
    }

    // event that is called if target enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetForCursor")
        {
            _target = collision.gameObject;
            // Debug.Log("enter");
        }
    }

    // event that is called if target exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetForCursor")
        {
            _target = null;
            // Debug.Log("exit");
        }
    }
}
