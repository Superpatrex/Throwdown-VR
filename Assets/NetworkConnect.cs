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
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("Joining");
    }
}
