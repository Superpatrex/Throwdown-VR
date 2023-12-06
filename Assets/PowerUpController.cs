using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpController : MonoBehaviour
{
    public int xMin, xMax; 
    public int zMin, zMax;
    public GameObject [] powerUp;
    public int xPos;
    public int zPos;
    public float minTime;
    public float maxTime;
    private float curTime;
    // bool spawn = false;
    private System.Random random;
    public AudioClip spawnedPowerUpSound;

    void Start()
    {
        this.curTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        curTime -= Time.deltaTime;

        if (curTime < 0f)
        {
            SpawnRandomPowerUp();
            PlaySound(this.spawnedPowerUpSound);
            curTime = UnityEngine.Random.Range(minTime, maxTime);
        }
    }

    private void SpawnRandomPowerUp()
    {
        xPos = UnityEngine.Random.Range(xMin, xMax);
        zPos = UnityEngine.Random.Range(zMin, zMax);

        Instantiate(PickRandomPowerUpPrefab(), new Vector3(xPos, 10, zPos), Quaternion.identity);
    }

    private GameObject PickRandomPowerUpPrefab()
    {
        return powerUp[UnityEngine.Random.Range(0, powerUp.Length)];
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null)
            return;
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}

