using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HittableObject : MonoBehaviour
{
    public float maxHealth = 10;
    public float health = 10;
    public float hitCooldown = 1;
    public ParticleSystem hitParticle = null;
    public AudioClip hitSound = null;
    public ParticleSystem deathParticle = null;
    public AudioClip deathSound = null;
    
    protected float hitTime;
    protected bool justHit = false;

    // Start is called before the first frame update
    void Start()
    {
        hitTime = 0;

        
        {
            ParticleSystem[] particleSystems = GameObject.Find("ParticleEffects").GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                if (particleSystem.name == "BloodParticle")
                {
                    hitParticle = particleSystem;
                    deathParticle = particleSystem;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        Vector3 force = direction.normalized * (0.6f * damage + 2f);
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        justHit = true;
        hitTime = hitCooldown;
        GetComponent<Rigidbody>().excludeLayers ^= 1 << 6;
        PlaySound(hitSound);
        if (health < 0)
        {
            DisplayParticle(deathParticle);
            PlaySound(deathSound);
            Destroy(gameObject);

            if (this.name == "Enemy Prefab(Clone)")
            {
                GameInformation.score += 5;
                this.GetComponent<Enemy>().Die();
            }
            
            EnemyController.numEnemies--;
            return;
        }
        DisplayParticle(hitParticle);
        if (this.name == "Enemy Prefab(Clone)")
            this.GetComponent<Enemy>().Stun(damage * 0.15f + 0.25f);
    }

    protected void PlaySound(AudioClip sound)
    {
        if (sound == null)
            return;
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

    protected void DisplayParticle(ParticleSystem particle)
    {
        if (particle == null)
            return;
        particle.transform.position = transform.position;
        particle.Stop();
        particle.Emit(100);
    }

    protected bool CheckIfSelf(DamageObject source)
    {
        XRDirectInteractor[] hands = GetComponentsInChildren<XRDirectInteractor>();
        if (hands == null)
            return false;
        for (int i = 0; i < hands.Length; i++)
        {
            List<IXRSelectInteractable> selected = hands[i].interactablesSelected;
            for (int j = 0; j < selected.Count; j++)
            {
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
