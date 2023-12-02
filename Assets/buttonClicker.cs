using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class buttonClicker : MonoBehaviour
{
    private Button button;
    public String key;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if(Input.GetButtonDown(key) && timer <= 0)
        {
            button.onClick.Invoke();
            timer = 2;
        }
    }
}
