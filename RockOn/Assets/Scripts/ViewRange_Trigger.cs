using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange_Trigger : MonoBehaviour
{
    // event that is called if target enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon")
        {
            collision.gameObject.GetComponent<Demon_Movement>().setIsInRange(true);
        }
    }

    // event that is called if target exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon")
        {
            collision.gameObject.GetComponent<Demon_Movement>().setIsInRange(false);
        }
    }
}
