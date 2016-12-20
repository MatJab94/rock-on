using System.Collections;
using UnityEngine;

public class Player_Regular_Attack_Trigger : MonoBehaviour
{
    // list that contains all enemies in range
    [HideInInspector]
    public ArrayList targets = new ArrayList();

    // event that is called if enemy enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // add this object to the list of enemies in range
            targets.Add(collision.gameObject);
        }
    }

    // event that is called if enemy exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // remove this object from the list of enemies in range
            targets.Remove(collision.gameObject);
        }
    }
}
