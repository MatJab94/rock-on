using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball_Attack : MonoBehaviour
{
    private int _currentDamage;
    private bool rythmFlag;

    // This fireball's health script
    private PlayerFireball_Health _fireballHealth;

    void Start()
    {
        _fireballHealth = GetComponentInParent<PlayerFireball_Health>();
        //_currentDamage = 1;
        //rythmFlag = true;
    }

    public void setDamageAndRythm(int damage, bool rythm)
    {
        _currentDamage = damage;
        rythmFlag = rythm;
    }

    // event that is called if player enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon")
        {
            collision.gameObject.GetComponentInParent<Demon_Health>().applyDamage(_currentDamage, false, false, rythmFlag);
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "Mag")
        {
            collision.gameObject.GetComponentInParent<Mag_Health>().applyDamage(_currentDamage, false, false, rythmFlag);
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "Fireball")
        {
            collision.gameObject.GetComponentInParent<Fireball_Health>().applyDamage(_currentDamage, false, false, rythmFlag);
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "Chest")
        {
            collision.gameObject.GetComponentInParent<Chest_Open>().hitChest();
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "ClosedDoor")
        {
            collision.gameObject.GetComponentInParent<ClosedDoor_Open>().hitDoor();
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.GetComponentInParent<Bomb_BlowUp>().hitBomb();
            _fireballHealth.applyDamage();
        }
        if (collision.gameObject.tag == "Wall")
        {
            _fireballHealth.applyDamage();
        }
    }
}
