using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEUnlocker_Unlock : MonoBehaviour
{
    // stuff to lock and unlock when this guitar object is on scene
    private Player_AoE_Attack _playerAoE;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerAoE = player.GetComponentInChildren<Player_AoE_Attack>();

        StartCoroutine(disableAoE());
    }

    IEnumerator disableAoE()
    {
        yield return new WaitForFixedUpdate();
        _playerAoE.aoeDisabled = true;
    }

    private void unlockAoE()
    {
        // let player use regular attack from now on
        _playerAoE.aoeDisabled = false;

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
