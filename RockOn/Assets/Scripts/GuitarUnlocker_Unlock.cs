using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarUnlocker_Unlock : MonoBehaviour
{
    // stuff to lock and unlock when this guitar object is on scene
    private Animator _playerAnim;
    private Player_Regular_Attack _playerAttack;
    public RuntimeAnimatorController guitarlessController;
    public RuntimeAnimatorController guitarController;

    void Start()
	{
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerAnim = player.GetComponent<Animator>();
        _playerAttack = player.GetComponentInChildren<Player_Regular_Attack>();

        _playerAnim.runtimeAnimatorController = guitarlessController;
        StartCoroutine(disableAttack());
    }

    IEnumerator disableAttack()
    {
        yield return new WaitForFixedUpdate();
        _playerAttack.attackDisabled = true;
    }

    private void unlockGuitar()
    {
        // change players animation to include guitar
        _playerAnim.runtimeAnimatorController = guitarController;

        // let player use regular attack from now on
        _playerAttack.attackDisabled = false;

        // destroy this guitar unlocker object
        Destroy(gameObject, 0.1f);
    }

    // event that is called if player enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            unlockGuitar();
        }
    }
}
