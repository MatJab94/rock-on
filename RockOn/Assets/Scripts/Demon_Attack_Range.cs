using UnityEngine;

public class Demon_Attack_Range : MonoBehaviour
{
    // flag shows if enemy is in range to attack the player
    private bool _canAttack = false;

    // Health Bar in GUI
    private Player_Health _playerHealth;

    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
    }

    void FixedUpdate()
    {
        if (_canAttack)
        {
            _playerHealth.applyDamage();
        }
    }

    // event that is called if enemy enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = true;
        }
    }

    // event that is called if enemy exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _canAttack = false;
        }
    }

}
