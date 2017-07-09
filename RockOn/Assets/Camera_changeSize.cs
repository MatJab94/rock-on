using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_changeSize : MonoBehaviour
{
    Camera cam;
	void Start()
	{
        cam = gameObject.GetComponent<Camera>();
	}

	void Update()
	{
        if (Input.GetKey(KeyCode.P))
        {
            cam.orthographicSize += 0.5f;
        }
        if (Input.GetKey(KeyCode.O))
        {
            cam.orthographicSize -= 0.5f;
        }
    }
}
