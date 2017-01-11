using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_BlowUp : MonoBehaviour
{
    // code script of this door
    private Bomb_Code _codeScript;

    // collider of this door
    private CircleCollider2D _collider;

    void Start()
    {
        _codeScript = GetComponentInChildren<Bomb_Code>();
        _collider = GetComponent<CircleCollider2D>();
    }

    // public method for easier calling of the method in codeScript
    public void hitBomb()
    {
        _codeScript.hitBomb();
    }

    public void blowUpBomb()
    {
        StartCoroutine(blowUpBombCoroutine());
    }

    // blows up the bomb
    IEnumerator blowUpBombCoroutine()
    {
        // TO-DO
        yield return new WaitForSeconds(0.1f);
    }
}
