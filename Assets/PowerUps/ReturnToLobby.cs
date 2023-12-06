using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ReturnToLobby : HittableObject
{
    void Awake()
    {
        
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Damage(float damage, Vector3 direction, DamageObject source)
    {        
        if (hitTime > 0)
            return;
        if (CheckIfSelf(source))
            return;

        health -= damage;
        Vector3 force = direction.normalized * (0.4f * damage + 2f);
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        justHit = true;
        hitTime = hitCooldown;
        GetComponent<Rigidbody>().excludeLayers ^= 1 << 6;
        PlaySound(hitSound);
        if (health < 0)
        {
            DisplayParticle(deathParticle);
            PlaySound(deathSound);

            health = 1;
            
            LoadStartMenu();
        }
        DisplayParticle(hitParticle);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }

    // This method is called when a new scene has finished loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Start Menu")
        {
            SceneManager.SetActiveScene(scene);
            Debug.Log("Start Menu scene loaded and set as active.");
        }
    }

    public IEnumerator SetActive(Scene scene)
    {
        int i = 0;
        while(i == 0)
        {
            i++;
            yield return null;
        }
        SceneManager.SetActiveScene(scene);
        yield break;
    }
}
