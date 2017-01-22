using UnityEngine;

public class GameManager : MonoBehaviour {

    public DeviceManager deviceManager;
    public float timeToStart = 1f;

	void Awake () {
        Invoke("StartTheGame", timeToStart);
	}
	
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
        {
            Debug.Log("game over");
            StopTheGame();
        }
	}


    void StartTheGame()
    {
        deviceManager.StartPlayers();
        Camera.main.GetComponent<CameraMovement>().moveCamera = true;
    }

    void StopTheGame()
    {
        Camera.main.GetComponent<CameraMovement>().moveCamera = false;
    }
}
