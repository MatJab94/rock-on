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

    // clef1 animator
    public Animator _clef1Anim;

    // clef2 animator
    public Animator _clef2Anim;

    // are the doors open?
    private bool _open;

    // you can specify what secret code it spawns with in the Inspector
    // 0 = red, 1 = green, 2 = blue, anything else = random
    public int[] secretCode;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _codeScript = GetComponentInChildren<ClosedDoor_Code>();
        _collider = GetComponent<BoxCollider2D>();

        _open = false;

        _codeScript.setClosedDoorCode(secretCode);
    }

    // public method for easier calling of the method in codeScript
    public void hitDoor()
    {
        _codeScript.hitDoor();
    }

    public void openDoor()
    {
        _open = true;

        StartCoroutine(openDoorCoroutine());
    }

    // opens the door
    IEnumerator openDoorCoroutine()
    {
        // small delay before opening
        yield return new WaitForSeconds(0.1f);

        // play opening animation
        _anim.SetTrigger("open");

        // delay for animation to end
        yield return new WaitForSeconds(1.5f);

        // disable the collider so the player can pass
        _collider.enabled = false;
    }

    public void activateDoor()
    {
        // start animating clefs
        _clef1Anim.SetBool("active", true);
        _clef2Anim.SetBool("active", true);

        // make the door interactable
        _codeScript.activateTheDoor();
    }

    public int[] getSecretCode()
    {
        return _codeScript.getSecretCode();
    }

    public bool isOpen()
    {
        return _open;
    }
}
