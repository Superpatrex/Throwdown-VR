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

    public typeOfPowerUp typePower = typeOfPowerUp.DAMAGE;

    void Awake()
    {
        
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
                
            }

            return;
        }
        DisplayParticle(hitParticle);
    }

}
