using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HittableObject : MonoBehaviour
{
    public float maxHealth = 10;
    public float health = 10;
    public float hitCooldown = 1;
    public ParticleSystem hitParticle = null;
    public ParticleSystem deathParticle = null;
    
    private float hitTime;
    private bool justHit = false;

    // Start is called before the first frame update
    void Start()
    {
        hitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (justHit)
        {
            hitTime -= Time.deltaTime;
            if (hitTime < 0)
            {
                justHit = false;
                GetComponent<Rigidbody>().excludeLayers ^= 1 << 6;
            }
        }
    }

    public void Damage(float damage, Vector3 direction, DamageObject source)
    {
        if (hitTime > 0)
            return;
        if (CheckIfSelf(source))
            return;

        health -= damage;
        Vector3 force = direction.normalized * (1.5f * damage + 1.25f);
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        justHit = true;
        hitTime = hitCooldown;
        GetComponent<Rigidbody>().excludeLayers ^= 1 << 6;
        GetComponent<AudioSource>().Play();
        if (health < 0)
        {
            DisplayParticle(deathParticle);
            Destroy(gameObject);
            return;
        }
        DisplayParticle(hitParticle);
    }

    private void DisplayParticle(ParticleSystem particle)
    {
        if (particle == null)
            return;
        particle.transform.position = transform.position;
        particle.Play();
    }

    private bool CheckIfSelf(DamageObject source)
    {
        XRDirectInteractor[] hands = GetComponentsInChildren<XRDirectInteractor>();
        Debug.Log("asas");
        if (hands == null)
            return false;
        for (int i = 0; i < hands.Length; i++)
        {
            Debug.Log("asas2");
            List<IXRSelectInteractable> selected = hands[i].interactablesSelected;
            for (int j = 0; j < selected.Count; j++)
            {
                Debug.Log("asas3");
                if (UnityEngine.Object.ReferenceEquals(selected[j].transform.gameObject, source.gameObject))
                    return true;
                else
                {
                    Debug.Log(selected[j].transform.gameObject);
                    Debug.Log(source.gameObject);
                }
            }
        }
        return false;
    }
}