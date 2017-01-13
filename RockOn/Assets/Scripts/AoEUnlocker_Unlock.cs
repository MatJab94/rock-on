using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEUnlocker_Unlock : MonoBehaviour
{
    // stuff to lock and unlock when this guitar object is on scene
    private Player_AoE_Attack _playerAoE;
    private Player_Mana _playerMana;
    private SpriteRenderer _mana;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject guiMana = GameObject.FindGameObjectWithTag("GUI_Mana");
        _mana = guiMana.GetComponent<SpriteRenderer>();
        _playerAoE = player.GetComponentInChildren<Player_AoE_Attack>();
        _playerMana = player.GetComponentInChildren<Player_Mana>();

        StartCoroutine(disableAoE());
    }

    IEnumerator disableAoE()
    {
        // have to wait for Start() of all the objects to finish
        yield return new WaitForFixedUpdate();

        //disable AoE attack
        _playerAoE.aoeDisabled = true;

        // disable mana in gui
        _mana.sprite = null;
    }

    private void unlockAoE()
    {
        // let player use regular attack from now on
        _playerAoE.aoeDisabled = false;

        // make gui sprite appear again
        _playerMana.updateGUI();

        // destroy this guitar unlocker object
        Destroy(gameObject, 0.1f);
    }

    // event that is called if player enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            unlockAoE();
        }
    }
}
