using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public TMP_Text scoreText;
    public TMP_Text modeText;
    void Start()
    {
        if (GameInformation.difficulty == 1)
        {
            modeText.text = "Easy Mode";
        }
        else if (GameInformation.difficulty == 2)
        {
            modeText.text = "Medium Mode";
        }
        else
        {
            modeText.text = "Hard Mode";
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.scoreText.text = "Score: " + GameInformation.score;
    }
}
