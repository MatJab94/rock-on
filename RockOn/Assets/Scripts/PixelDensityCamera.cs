using UnityEngine;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour
{

    public float pixelsToUnits = 100;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        cam.orthographicSize = Screen.height / pixelsToUnits / 2;
    }
}
