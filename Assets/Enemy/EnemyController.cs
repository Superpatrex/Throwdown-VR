using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class EnemyController : MonoBehaviour
{
    public GameObject [] enemyTypes;
    public float minSpawnTime = 20;
    public float maxSpawnTime = 10;
    public int maxEnemies = 1;
    public float curSpawnime;
    private System.Random random;
    public bool startEnemies = false;
    public static int numEnemies;
    public static float enemyMovementSpeedMultiplier = 1f;

    public enum GameMode
    {
        EASY,
        MEDIUM,
        HARD
    };

    private static Point3D [] spawnPoints = 
    {
        new Point3D(4f, 1f, 7f),
        new Point3D(7f, 1f, 4f),
        new Point3D(-7f, 1f, 4f),
        new Point3D(7f, 1f, -4f),
        new Point3D(-7f, 1f, -4f),
    };

    void Start()
    {
        if (GameInformation.difficulty == 1)
        {
            SetEasyMode();
        }
        else if (GameInformation.difficulty == 2)
        {
            SetMediumMode();
        }
        else if (GameInformation.difficulty == 3)
        {
            SetHardMode();
        }

        StartEnemies();
        Debug.Log("Enemies are beginning to spawn");
    }

    public void StartEnemies()
    {
        this.startEnemies = true;
    }

    public void StopEnemies()
    {
        this.startEnemies = false;
    }

    void Update()
    {
        if (!this.startEnemies)
        {
            return;
        }

        if (numEnemies <= maxEnemies)
        {
            curSpawnime -= Time.deltaTime;

            if (curSpawnime < 0f)
            {
                SpawnEnemies();
                Debug.Log("Spawned an enemy");
                curSpawnime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
                numEnemies++;
            }

        }
    }

    private void SpawnEnemies()
    {
        Point3D enemySpawnLocation = PickRandomSpawnLocation();

        GameObject temp = Instantiate(PickRandomEnemy(), new Vector3(enemySpawnLocation.x, enemySpawnLocation.y, enemySpawnLocation.z), Quaternion.identity);

        temp.transform.position = transform.position;
        temp.transform.position = new Vector3(enemySpawnLocation.x, enemySpawnLocation.y, enemySpawnLocation.z);
    }

    private GameObject PickRandomEnemy()
    {
        return enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
    }

    private Point3D PickRandomSpawnLocation()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound == null)
            return;
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

    public void SetEasyMode()
    {
        enemyMovementSpeedMultiplier = 1f;
        this.maxEnemies = 2;
        this.minSpawnTime = 20;
        this.maxSpawnTime = 30;
    }

    public void SetMediumMode()
    {
        enemyMovementSpeedMultiplier = 1.8f;
        this.maxEnemies = 4;
        this.minSpawnTime = 15;
        this.maxSpawnTime = 25;
    }

    public void SetHardMode()
    {
        enemyMovementSpeedMultiplier = 3.1f;
        this.maxEnemies = 6;
        this.minSpawnTime = 10;
        this.maxSpawnTime = 20;
    }
}

public class Point3D
{
    public float x, y, z;

    public Point3D(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    Point3D()
    {
        
    }

}

