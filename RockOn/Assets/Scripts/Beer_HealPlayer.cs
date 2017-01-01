using UnityEngine;

public class Beer_HealPlayer : MonoBehaviour
{
    // event that is called if player enters this Object's collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().healPlayer(1);

            Destroy(gameObject);
        }
    }
}
