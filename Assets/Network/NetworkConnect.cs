using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;

public class NetworkConnect : MonoBehaviour
{
    public void Create()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("Creating");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Arena");
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("Joining");
    }

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start Menu");
    }
}
