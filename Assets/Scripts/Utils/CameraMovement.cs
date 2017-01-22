using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float cameraSpeed;
    public float pixelsPerUnit = 30f;
    public bool moveCamera;
    public Vector2 mapSize;
    Vector2 cameraSize;

	void Awake () {
		
	}

    public void SetMapSize(Vector2 value)
    {
        cameraSize = new Vector2();
        mapSize = value;
        cameraSize.x = (Camera.main.pixelWidth) / pixelsPerUnit;
        cameraSize.y = Camera.main.pixelHeight;
        Debug.Log(cameraSize);
        Debug.Log(mapSize);
    }
	
	void Update () {
        if (moveCamera && (transform.position.x + cameraSize.x < mapSize.x))
        {
            transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
        }
    }
}
