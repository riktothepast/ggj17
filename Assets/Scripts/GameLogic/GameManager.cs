using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    public GameObject winCanvas;
    public DeviceManager deviceManager;
    public float timeToStart = 1f;
    bool hasGameStarted = false;

	void Awake () {
        Invoke("StartTheGame", timeToStart);
	}
	
	void Update () {
        if (!hasGameStarted)
            return;

        if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
        {
            Debug.Log("game over");
            StopTheGame();
            Invoke("GameOver", 5);
        }
        if (GameObject.FindGameObjectsWithTag("VampiresHearth").Length == 0)
        {
            winCanvas.SetActive(true);
            Invoke("GameOver", 5);
        }
    }


    void StartTheGame()
    {
        deviceManager.StartPlayers();
        Camera.main.GetComponent<CameraMovement>().moveCamera = true;
        hasGameStarted = true;
    }

    void StopTheGame()
    {
        Camera.main.GetComponent<CameraMovement>().moveCamera = false;
    }
    
    void GameOver()
    {
        SceneManager.LoadScene("endingScene");
    }
}
