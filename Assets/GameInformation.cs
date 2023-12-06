using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{
    public static int difficulty = 1;
    public TextMeshProUGUI difficultyDisplay;
    public AudioClip easyMode;
    public AudioClip mediumMode;
    public AudioClip hardMode; 

    private bool scenesLoaded = false;

    public Player player;

    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDifficulty()
    {

        switch (difficultyDisplay.text)
        {
            case "Difficulty: Easy":
                PlaySound(this.easyMode);
                difficulty = 1;
                break;
            case "Difficulty: Medium":
                PlaySound(this.mediumMode);
                difficulty = 2;
                break;
            default:
                PlaySound(this.hardMode);
                difficulty = 3;
                break;
        }
        Debug.Log(difficulty);
    }

    // There is a bug when in the editor things get dark. This is fixed on an actual build.
    public void StartGame()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }

    // This method is called when a new scene has finished loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Arena")
        {
            SceneManager.SetActiveScene(scene);
            Debug.Log("Arena scene loaded and set as active.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null)
            return;
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

}
