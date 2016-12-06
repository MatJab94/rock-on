using UnityEngine;
using System.Collections;

public class Player_Attack : MonoBehaviour
{

    private ArrayList _targets = new ArrayList();
    private bool _isAttacking = false;

    public void Update()
    {
        if (Input.GetAxisRaw("Attack1") != 0)
        {
            if (_isAttacking == false)
            {
                
                foreach (GameObject target in _targets)
                {
                    target.SendMessage("applyDamage");
                }
                _isAttacking = true;
            }
        }

        if (Input.GetAxisRaw("Attack1") == 0)
        {
            _isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _targets.Remove(collision.gameObject);
        }
    }
}
