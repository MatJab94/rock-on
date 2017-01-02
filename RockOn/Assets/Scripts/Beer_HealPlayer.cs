using UnityEngine;

public class Beer_HealPlayer : MonoBehaviour
{
    private Player_Health _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(_playerHealth.getHealth() < _playerHealth.getMaxHealth())
            {
                _playerHealth.healPlayer(1);
                
                Destroy(gameObject, 0.05f);
            }
        }
    }
}
