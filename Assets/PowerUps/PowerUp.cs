using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : HittableObject
{
    public enum typeOfPowerUp
    {
        DAMAGE,
        JUMP,
        BIG,
        SPEED
    }

    private String playerName = "/Player Prefab/GorillaPlayer";

    private static ParticleSystem[] particleSystems;

    public typeOfPowerUp typePower = typeOfPowerUp.DAMAGE;

    void Awake()
    {
        particleSystems = GameObject.Find("ParticleEffects").GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.name == "DamagePowerUp" && typePower == typeOfPowerUp.DAMAGE)
            {
                hitParticle = particleSystem;
                deathParticle = particleSystem;
            }
            else if (particleSystem.name == "JumpPowerUp" && typePower == typeOfPowerUp.JUMP)
            {
                hitParticle = particleSystem;
                deathParticle = particleSystem;
            }
            else if (particleSystem.name == "BigPowerUp" && typePower == typeOfPowerUp.BIG)
            {
                hitParticle = particleSystem;
                deathParticle = particleSystem;
            }
            else if (particleSystem.name == "SpeedPowerUp" && typePower == typeOfPowerUp.SPEED)
            {
                hitParticle = particleSystem;
                deathParticle = particleSystem;
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
            Destroy(gameObject);

            GameObject player = GameObject.Find(playerName);

            if (typePower != typeOfPowerUp.DAMAGE)
            {
                player.GetComponent<GorillaLocomotion.Player>().SetPowerUp(typePower);
            }
            else if (typePower == typeOfPowerUp.DAMAGE)
            {
                DamageObject.damage *= 3.0f;
            }

            return;
        }
        DisplayParticle(hitParticle);
    }

}
