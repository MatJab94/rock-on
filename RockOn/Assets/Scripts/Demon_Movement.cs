using UnityEngine;
using System.Collections;

public class Demon_Movement : MonoBehaviour
{
    private float _speed; // movement speed
    float speedModifier; // for slowing down etc
    private float _minRange; // min range at which it stops chasing the target (it's too close)
    private Transform _target; // current target's position
    private Rigidbody2D _rb; // this objects's rigidbody2d
    private Animator _anim; // this object's animator
    private Demon_Attack_Range _attackScript; // for changing speed when attacking
    private float _distance; // distance between enemy and target
    private float _pushBackPower; // how strong is enemy pushed back when pick is active
    private bool _isInRange; // is enemy in range of the player's view?
    private Enemy_Audio _ea;  // C'mon! sounds
    private Vector3[] path; // path to the target
    int targetIndex; // index of Node in path
    float epsilon; // to check if close enough to a Node in path
    private bool lookingForPath; // is path finding algorithm running
    private bool canMove; // when path was found and target is in range
    private Vector3 currentWaypoint; // waypoint to move to
    private bool isMoving = false; // flag for animating in rythm
    // stuff for drawing gizmos and debugging
    private Vector2 currentDirection;
    public bool drawPath;
    private AStar_Grid grid;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _attackScript = GetComponentInChildren<Demon_Attack_Range>();
        _ea = GetComponent<Enemy_Audio>();

        _speed = 2.0f;
        _minRange = 0.67f;
        _pushBackPower = 15.0f;
        _isInRange = false;
        epsilon = 0.05f;
        lookingForPath = false;
        path = null;
        canMove = false;
        currentWaypoint = transform.position;
    }

    private void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("AStar").GetComponent<AStar_Grid>();
    }

    public void onPathFound(Vector3[] newPath, bool pathSuccess)
    {
        // if path was successfuly found
        if (pathSuccess)
        {
            StopCoroutine(followPath()); // stop following old path
            path = newPath; // change the path
            StartCoroutine(followPath()); // and follow it
        }

        // set flags to search for the path again and/or start moving
        lookingForPath = false;
        canMove = pathSuccess;
    }

    public IEnumerator followPath()
    {
        // start with the first waypoint, if it exists
        if (path.Length >= 1)
            currentWaypoint = path[0];

        // follow the path
        while (true)
        {
            if (Vector2.Distance(transform.position, currentWaypoint) <= epsilon)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    // stop when reached the target waypoint
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        // chase player only if in range
        if (_isInRange)
        {
            // calculate distance between enemy and target (player)
            _distance = Vector2.Distance(transform.position, _target.position);

            // stop chasing when close to player
            if (_distance >= _minRange)
            {
                // start looking for the path and move
                if (!lookingForPath)
                {
                    lookingForPath = true;

                    // find the path to target
                    //pathManager.startFindPath(transform.position, _target.position);
                    AStar_PathRequestManager.requestPath(transform.position, _target.position, onPathFound);
                }
            }
            else
            {
                // object not moving, stop animation
                isMoving = false;
                canMove = false;
            }
        }
        else
        {
            // object not moving, stop animation
            isMoving = false;
            canMove = false;
        }

        ////////////////////////////////////////////////////////////////
        // if there is a path and demon can move
        if (canMove)
        {
            // moving, animate
            isMoving = true;

            //play C'mon! sound
            StartCoroutine(Wait(_ea, 2));

            // modify speed if attacking
            if (_attackScript.isCooldown()) speedModifier = 0.5f;
            else speedModifier = 1.0f;

            Vector2 direction = new Vector2(currentWaypoint.x - transform.position.x, currentWaypoint.y - transform.position.y).normalized;

            currentDirection = direction; // to draw gizmos

            _rb.AddForce(direction * _speed * speedModifier, ForceMode2D.Impulse);
        }
    }

    // called when enemy is attacked and pick power-up is active
    public void pushBack()
    {
        // calculate direction (and normalize it so it doesn't change the speed of movement)
        Vector2 direction = new Vector2(transform.position.x - _target.position.x, transform.position.y - _target.position.y).normalized;

        _rb.AddForce(direction * _speed * _pushBackPower, ForceMode2D.Impulse);
    }

    // for playing "Come On!"
    IEnumerator Wait(Enemy_Audio _ea, float delay)
    {
        _ea.PlayComeOn();
        _ea.enabled = true;
        yield return new WaitForSeconds(delay);
        _ea.enabled = false;
    }

    public void setIsInRange(bool isInRange)
    {
        _isInRange = isInRange;
    }

    // objects need to subscribe and unsubscribe from events when they're enabled/disabled
    private void OnEnable()
    {
        RythmBattle.OnGoodRythm += rythmAnimation;
    }
    private void OnDisable()
    {
        RythmBattle.OnGoodRythm -= rythmAnimation;
    }

    // animate player on rythm events
    void rythmAnimation()
    {
        if (isMoving)
        {
            _anim.SetTrigger("beat");
        }
    }

    // draw the path of the demon in Unity Editor
    private void OnDrawGizmos()
    {

        if (drawPath && path != null && grid != null)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i + 1 < path.Length; i++)
            {
                Gizmos.DrawSphere(path[i + 1], 0.09f);
                Gizmos.DrawLine(path[i], path[i + 1]);
            }

            // draw current node
            Node n = grid.nodeFromWorldPoint(transform.position);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(n.worldPosition, Vector3.one * 0.1f);

            // draw direction where demon is going to move
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + currentDirection.x, transform.position.y + currentDirection.y, transform.position.z));
        }
    }
}
