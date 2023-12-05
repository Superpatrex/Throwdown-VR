using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkConnect : MonoBehaviour
{
    public void Create()
    {
        Debug.Log("Creating");
        SceneManager.LoadScene("Arena", LoadSceneMode.Additive);
        NetworkManager.Singleton.StartHost();
        SceneManager.UnloadScene("Start Menu");
    }

    public void Join()
    {
        Debug.Log("Joining");
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
        NetworkManager.Singleton.StartClient();
    }

    public void Disconnect()
    {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
        NetworkManager.Singleton.Shutdown();
    }
}
