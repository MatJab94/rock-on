﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_BlowUp : MonoBehaviour
{
    // code script of this door
    private Bomb_Code _codeScript;

    private Bomb_AoE_Attack _attackScript;

    void Start()
    {
        _codeScript = GetComponentInChildren<Bomb_Code>();
        _attackScript = GetComponentInChildren<Bomb_AoE_Attack>();
    }

    // public method for easier calling of the method in codeScript
    public void hitBomb()
    {
        _codeScript.hitBomb();
    }

    public void blowUpBomb(bool damagePlayer)
    {
        // start the attack and pass the flag (should player get damaged or enemies?)
        _attackScript.aoeAttack(damagePlayer);
    }

    public void killBomb()
    {
        // there's some bugs when destroying the object immediately,
        // so I'm moving it somewhere else and killing it after a second
        gameObject.transform.position = new Vector3(-10000, -10000, -10000);
        Destroy(gameObject, 0.5f);
    }
}
