using UnityEngine;

public class Beer_HealPlayer : MonoBehaviour
{
    private Player_Health _playerHealth;

    // this GameObject's AudioSource component
    private AudioSource _audioSource;

    // audio file with beer grabbing sound
    public AudioClip beerGrab;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        _audioSource = GetComponent<AudioSource>();
    }

    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(_playerHealth.getHealth() < _playerHealth.getMaxHealth())
            {
                // play grabbing sound
                _audioSource.PlayOneShot(beerGrab);

                _playerHealth.healPlayer(1);

                this.transform.position = new Vector3(-10000, -10000, -10000);

                Destroy(gameObject, 3.0f);
            }
        }
    }
}
