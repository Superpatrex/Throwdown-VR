using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : HittableObject
{
    public enum typeOfPowerUP
    {
        DAMAGE,
        JUMP,
        BIG,
        SPEED
    }



    public typeOfPowerUP type = typeOfPowerUP.DAMAGE;

    void Awake()
    {
        this.maxHealth = .1f;
        this.health = .1f;

        String typeString = null;

        if (type == typeOfPowerUP.DAMAGE)
        {
            typeString = "DamagePowerUp";
        }
        else if (type == typeOfPowerUP.JUMP)
        {
            typeString = "JumpPowerUp";
        }
        else if (type == typeOfPowerUP.SPEED)
        {
            typeString = "SpeedPowerUp";
        }
        else if (type == typeOfPowerUP.BIG)
        {
            typeString = "BigPowerUp";
        }

        this.hitParticle = getParticleSystem(typeString);
    }

    private ParticleSystem getParticleSystem(String type)
    {
        ParticleSystem [] ps = GameObject.Find("ParticleEffects").GetComponents<ParticleSystem>();

        for (int i = 0; i < ps.Length; i++)
        {
            if (ps[i].gameObject.name == type)
            {
                return ps[i];
            }
        }

        return null;
    }
}
