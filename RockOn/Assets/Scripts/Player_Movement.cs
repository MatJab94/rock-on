using UnityEngine;
using System.Collections;


/*
 * A script to move the player with Inputs set in InputManager
 * You can change inputs in Unity: Edit -> Project Settings -> Input
 * 
 * The Player needs a RigidBody2D
 */
public class Player_Movement : MonoBehaviour
{
    // Speed of the movement, change it in the Inspector
    public float speed;

    // This object's RigidBody2D component
    private Rigidbody2D _rb;
    private Animator _anim;

    void Start()
    {
        // Get this object's RigidBody
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Move Character based on inputs, set in InputManager
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement != Vector2.zero)
        {
            _anim.SetBool("isMoving", true);
            _rb.AddForce(movement * speed, ForceMode2D.Impulse);
        }
        else
        {
            _anim.SetBool("isMoving", false);
        }
    }
}
