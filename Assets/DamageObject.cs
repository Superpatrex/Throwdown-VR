using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;

public class DamageObject : MonoBehaviour
{
    public static float damageMultiplier = 1f;
    public float damage = 3f;
    public bool isHeld = false;
    public GameObject hitDirection;
    public bool enemyWeapon = false;

    public bool damageEnabled = false;
    private float disableTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyWeapon)
            damageEnabled = true;
        GetComponent<Rigidbody>().excludeLayers = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyWeapon)
            return;

        if (!damageEnabled || isHeld)
            return;

        disableTime += Time.deltaTime;
        if (disableTime > 2)
        {
            GetComponent<Rigidbody>().excludeLayers ^= (1 << 9);
            damageEnabled = false;
        }
    }

    public void Hold()
    {
        isHeld = true;
        damageEnabled = true;
        GetComponent<Rigidbody>().excludeLayers ^= (1 << 9);
    }
    
    public void Drop()
    {
        isHeld = false;
        disableTime = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("a");
        if (!damageEnabled)
            return;
        Debug.Log("b");
        HittableObject hittableObject = other.gameObject.GetComponent<HittableObject>();
        if (hittableObject == null)
            return;
        Debug.Log("c");

        Vector3 dir = hitDirection.transform.forward.normalized;
        dir.y = 0;
        dir = dir.normalized;
        dir.y = 0.6f;

        if (hittableObject.GetType() == typeof(PowerUp))
        {
            ((PowerUp)hittableObject).Damage(damage, dir, this);
            Debug.Log("11");
        }
        else if (hittableObject.GetType() == typeof(ReturnToLobby))
        {
            ((ReturnToLobby)hittableObject).Damage(damage, dir, this);
            Debug.Log("12");
        }
        else if (hittableObject.name == "Enemy Prefab(Clone)")
        {
            GameInformation.score++;
            hittableObject.Damage(damage, dir, this);
            Debug.Log("13");
        }
        else
        {
            hittableObject.Damage(damage, dir, this);
            Debug.Log("14");
        }
    }
}
