using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_AoE_Attack : MonoBehaviour
{
    // sprites with range "animation", set in Inspector
    public Sprite[] _sprites;

    // list that contains all targets (gameObjects) in range
    private ArrayList _targets;

    // this object's sprite renderer (for "animating" the range when attacking)
    private SpriteRenderer _sr;

    // this transform for changing scale ("animating")
    private Transform _tf;

    // Bomb_BlowUp script for killing the bomb object after it blows up
    private Bomb_BlowUp _bombScript;

    public void Start()
    {
        _targets = new ArrayList();
        _sr = GetComponent<SpriteRenderer>();
        _tf = GetComponent<Transform>();
        _tf.localScale = Vector3.zero;
        _bombScript = GetComponentInParent<Bomb_BlowUp>();
    }

    // called when bomb explodes
    public void aoeAttack(bool damagePlayer)
    {
        // change sprite of the range depending on if bomb hits enemies or player
        updateSprite(damagePlayer);

        // play attack's sound !!!TO-DO!!!
        //_playerAudio.playChordSound();

        // animates the range while attacking and applies damage to targets
        StartCoroutine(animateAndAttack(damagePlayer));
    }

    private void updateSprite(bool damagePlayer)
    {
        // change sprite based on whether it damages player or enemies
        if (damagePlayer)
        {
            _sr.sprite = _sprites[1];
        }
        else
        {
            _sr.sprite = _sprites[0];
        }
    }

    private void attackTargets(bool damagePlayer)
    {
        foreach (GameObject target in _targets)
        {
            if (target.tag == "Demon" && !damagePlayer)
            {
                target.GetComponent<Demon_Health>().applyDamage(2, true);
            }
            if (target.tag == "Mag" && !damagePlayer)
            {
                target.GetComponent<Mag_Health>().applyDamage(2, true);
            }
            if (target.tag == "Fireball" && !damagePlayer)
            {
                target.GetComponent<Fireball_Health>().applyDamage(2, false, true);
            }
            if (target.tag == "Player" && damagePlayer)
            {
                target.GetComponent<Player_Health>().applyDamage(2);
            }
        }
    }

    // highlights the collider while attacking
    IEnumerator animateAndAttack(bool damagePlayer)
    {
        Color c = Color.white;
        Vector3 scale = Vector3.zero;

        _sr.color = c;
        _tf.localScale = scale;

        // scales the range UP
        for (float f = 0.0f; f <= 2.0f; f += Time.deltaTime * 7)
        {
            scale.x = f;
            scale.y = f;
            _tf.localScale = scale;
            yield return null;
        }

        // apply damage to every enemy in range after range's scale is maximum
        attackTargets(damagePlayer);

        // fades range to transparent
        for (float f = 1.0f; f >= 0.0f; f -= Time.deltaTime * 3)
        {
            c.a = f;
            _sr.color = c;
            yield return null;
        }

        _tf.localScale = Vector3.zero;

        _bombScript.killBomb();
    }

    // event that is called if target enters this Object's collider (is in range)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon" || collision.gameObject.tag == "Mag" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Player")
        {
            // add this object to the list of targets in range
            _targets.Add(collision.gameObject);
        }
    }

    // event that is called if target exits this Object's collider (is out of range)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Demon" || collision.gameObject.tag == "Mag" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Player")
        {
            // remove this object from the list of targets in range
            _targets.Remove(collision.gameObject);
        }
    }
}
