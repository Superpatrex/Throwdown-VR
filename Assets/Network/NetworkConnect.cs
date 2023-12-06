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
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
        NetworkManager.Singleton.StartHost();
    }

    public void Join()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
        NetworkManager.Singleton.StartClient();
    }

    public void Disconnect()
    {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
        NetworkManager.Singleton.Shutdown();
    }
}
