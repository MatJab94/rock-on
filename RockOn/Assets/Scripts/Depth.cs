using UnityEngine;
using System.Collections;

public class Depth : MonoBehaviour
{

    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.y);
    }
}
