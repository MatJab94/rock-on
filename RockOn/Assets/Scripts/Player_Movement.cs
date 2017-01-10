using UnityEngine;

/*
 * A script to move the player with Inputs set in InputManager
 * You can change inputs in Unity: Edit -> Project Settings -> Input
 * 
 * The Player needs a RigidBody2D to work with physics
 * 
 * Movement is connected with the animation, script triggers
 * flags changes that determine current animation status
 */
public class Player_Movement : MonoBehaviour
{
    // Speed of the movement
    private float _speed;

    // This object's RigidBody2D component, for physics (like colliding with objects)
    private Rigidbody2D _rb;

    // This object's Animator component, to animate when walking
    private Animator _anim;

    void Start()
    {
        // initialising variables with this object's components
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _anim.SetBool("isMoving", false);

        _speed = 2.75f;
    }

    // using FixedUpdate for constant movement speed, regardless of framerate
    void FixedUpdate()
    {
        // Move Character based on inputs, set in InputManager
        // GetAxisRaw returns 0 if button is not pressed, and 1 if it's pressed
        Vector2 movement = new Vector2(Input.GetAxisRaw("Move_Horizontal"), Input.GetAxisRaw("Move_Vertical"));

        // if we are moving
        if (movement != Vector2.zero)
        {
            // tell the Animator we are moving, start movement animation
            _anim.SetBool("isMoving", true);

            // move character based on movement vector and speed
            _rb.AddForce(movement * _speed, ForceMode2D.Impulse);
        }
        else
        {
            // tell the Animator we are NOT moving, start idle animation
            _anim.SetBool("isMoving", false);
        }
    }
}
