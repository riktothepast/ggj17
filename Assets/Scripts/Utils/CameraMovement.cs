using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float cameraSpeed;
    public bool moveCamera;

	void Awake () {
		
	}
	
	void Update () {
        if (moveCamera)
        {
            transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
        }
    }
}
