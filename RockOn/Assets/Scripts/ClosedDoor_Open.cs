using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor_Open : MonoBehaviour
{
    // this object's animator
    private Animator _anim;

    // code script of this door
    private ClosedDoor_Code _codeScript;

    // sprite renderers of the keys objects
    private SpriteRenderer[] _keysSR;

    // collider of this door
    private BoxCollider2D _collider;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _codeScript = GetComponentInChildren<ClosedDoor_Code>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // public method for easier calling of the method in codeScript
    public void hitDoor()
    {
        _codeScript.hitDoor();
    }

    public void openDoor()
    {
        StartCoroutine("openDoorCoroutine");
    }

    // opens the door
    IEnumerator openDoorCoroutine()
    {
        // small delay before opening
        for (float f = 0.1f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        // play opening animation
        _anim.SetTrigger("open");

        // delay for animation to end
        for (float f = 1.5f; f >= 0; f -= Time.deltaTime)
        {
            yield return null;
        }

        // disable the collider so the player can pass
        _collider.enabled = false;
    }
}
