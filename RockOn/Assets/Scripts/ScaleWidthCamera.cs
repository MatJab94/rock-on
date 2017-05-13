using UnityEngine;

[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour
{

    public int targetWidth = 640;
    public float pixelsToUnits = 100;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        cam.orthographicSize = height / pixelsToUnits / 2;
    }
}
