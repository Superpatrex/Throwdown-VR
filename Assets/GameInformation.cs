using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{
    public int difficulty = 1;
    public TextMeshProUGUI difficultyDisplay;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDifficulty()
    {
        if (++difficulty > 3)
            difficulty = 1;

        switch (difficulty)
        {
            case 1:
                difficultyDisplay.text = "Difficulty: Easy";
                break;
            case 2:
                difficultyDisplay.text = "Difficulty: Medium";
                break;
            default:
                difficultyDisplay.text = "Difficulty: Hard";
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }
}
