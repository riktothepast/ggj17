using UnityEngine;
using InControl;
using System.Collections.Generic;

public class DeviceManager : MonoBehaviour {
    public GameObject playerPrefab;
    public List<CharacterLogic> players;

	public void StartPlayers () {
        players = new List<CharacterLogic>();
        for (int x = 0; x < InputManager.Devices.Count; x++)
        {
            CreateNewPlayer(InputManager.Devices[x]);
        }
        InputManager.OnDeviceDetached += inputDevice => DeviceDetached(inputDevice);
        InputManager.OnDeviceAttached += inputDevice => DeviceAttached(inputDevice);
    }

    void DeviceDetached(InputDevice device)
    {
        Debug.Log(device.Name + " was detached");
        foreach (CharacterLogic player in players)
        {
            if (player.playerController.Device == device)
            {
                players.Remove(player);
                Destroy(player.gameObject);
                break;
            }
        }
    }

    void DeviceAttached(InputDevice device)
    {
        Debug.Log(device.Name + " was attached");
        CreateNewPlayer(device);
    }

    void CreateNewPlayer(InputDevice device)
    {
        CharacterLogic player = Instantiate(playerPrefab).GetComponent<CharacterLogic>();
        player.InitPlayer(device);
        players.Add(player);
    }

}
