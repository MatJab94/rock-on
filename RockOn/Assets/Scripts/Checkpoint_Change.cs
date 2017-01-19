using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Change : MonoBehaviour
{
    private Player_Health _playerScript;

	void Start()
	{
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        GetComponent<SpriteRenderer>().sprite = null;
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerScript.setRespawnPosition(gameObject.transform.position);

            Destroy(gameObject, 0.05f);
        }
    }
}
