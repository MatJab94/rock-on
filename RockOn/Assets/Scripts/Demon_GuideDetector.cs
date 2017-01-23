using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_GuideDetector : MonoBehaviour
{
    private Demon_Movement _moveScript;

    private void Start()
    {
        _moveScript = GetComponentInParent<Demon_Movement>();
    }

    // event that is called if target enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Guide")
        {
            _moveScript.setGuideDetected(collision.gameObject.transform);
            //Debug.Log("Guide detected");
        }
    }

    // event that is called if target exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Guide")
        {
            //Debug.Log("Guide lost");
            //collision.gameObject.GetComponent<Demon_Movement>().setIsInRange(false);
        }
    }
}
