using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Speed of the movement, set in Inspector
    public float _speed;

    // buttons pressed by player
    Vector2 input = Vector2.zero;

    // This object's RigidBody2D component, for physics (like colliding with objects)
    private Rigidbody2D _rb;

    // This object's Animator component, to animate when walking
    private Animator _anim;

    // flags for animating in rythm on odd and even beats
    bool isMoving = false, beatEven = false;

    // audio file with footsteps sound used as "metronome"
    public AudioClip footsteps;

    // this GameObject's AudioSource component
    private AudioSource _audioSource;

    void Start()
    {
        // initialising variables with this object's components
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // using FixedUpdate for constant movement speed, regardless of framerate
    void FixedUpdate()
    {
        // Move Character based on inputs, set in InputManager
        // GetAxisRaw returns 0 if button is not pressed, and 1 if it's pressed
        input = new Vector2(Input.GetAxisRaw("Move_Horizontal"), Input.GetAxisRaw("Move_Vertical"));

        // if there's input, player is moving
        if (input != Vector2.zero)
        {
            isMoving = true;
            // move character based on movement vector and speed
            _rb.AddForce(input * _speed, ForceMode2D.Impulse);
        }
        else
        {
            isMoving = false;
        }
    }

    // objects need to subscribe and unsubscribe from events when they're enabled/disabled
    private void OnEnable()
    {
        RythmBattle.OnBeat += rythmAnimation;
    }
    private void OnDisable()
    {
        RythmBattle.OnBeat -= rythmAnimation;
    }

    // animate player on rythm events while moving
    void rythmAnimation()
    {
        // change flag to remember if beat is even or odd
        beatEven = !beatEven;
        if (isMoving)
        {
            // play footstep sound
            _audioSource.PlayOneShot(footsteps);

            // different animation frame for every other beat
            if (beatEven) _anim.SetTrigger("beatEven");
            else _anim.SetTrigger("beatOdd");
        }
    }
}
