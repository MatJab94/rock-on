using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClefColumn_Range : MonoBehaviour
{
    // script that plays the code sequence that opens the door
    private ClefColumn_PlayCode _ClefColumnScript;

    // rythm battle script
    private RythmBattle _rythmBattle;

    private void Start()
    {
        _ClefColumnScript = GetComponentInParent<ClefColumn_PlayCode>();
        _rythmBattle = GameObject.FindGameObjectWithTag("RythmBattle").GetComponent<RythmBattle>();
    }

    // when player is in range activate the Clef and the Door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // disable the collider, this event needs to happen only once
            GetComponent<CircleCollider2D>().enabled = false;

            StartCoroutine(Animate());
        }
    }

    IEnumerator Animate()
    {
        while (true)
        {
            if (getRythm())
            {
                // activate the objects
                _ClefColumnScript.activateClefAndDoor();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    // getter to easily get the flag
    private bool getRythm()
    {
        return _rythmBattle.getRythmFlag();
    }
}
