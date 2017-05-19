using UnityEngine;

public class Cursor_Movement : MonoBehaviour {

    // cursor's position
    Transform tf;

    // main camera is needed to translate mouse position on the screen to in-game world position
    Camera mainCamera;

    // X and Y position on the screen ([0,0] is bottom-left, [1,1] is top-right)
    float x;
    float y;

    // value to multiply with X and Y coordinates from Input.mousePosition
    // X and Y needs to be normalised (to be between 0 and 1) to be used
    float xNormalise;
    float yNormalise;

    // Use this for initialization
    void Start () {
        tf = gameObject.GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        // mousePosition returns position on the screen (actual pixels of the window)
        // they need to be normalised to be translated to position in game's world
        x = Input.mousePosition.x * xNormalise;
        y = Input.mousePosition.y * yNormalise;

        // final position of the cursor
        tf.position = mainCamera.ViewportToWorldPoint(new Vector3(x, y, mainCamera.nearClipPlane));
        
        // Debug.Log("position is [ " + tf.position.x + " ; " + tf.position.y + " ; " + tf.position.z + " ]");
    }

    private void FixedUpdate()
    {
        // calculate normalising value, for better performance (multiplying is better than division)
        xNormalise = 1.0f / Screen.width;
        yNormalise = 1.0f / Screen.height;
    }
}
