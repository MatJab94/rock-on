using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClefColumn_Range : MonoBehaviour
{
    // script that plays the code sequence that opens the door
    private ClefColumn_PlayCode _ClefColumnScript;

    private void Start()
    {
        _ClefColumnScript = GetComponentInParent<ClefColumn_PlayCode>();
    }

    // when player is in range activate the Clef and the Door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // disable the collider, this event needs to happen only once
            GetComponent<CircleCollider2D>().enabled = false;

            // activate the objects
            _ClefColumnScript.activateClefAndDoor();
        }
    }
}
