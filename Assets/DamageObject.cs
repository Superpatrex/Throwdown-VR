using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;

public class DamageObject : MonoBehaviour
{
    public float damage = 1;
    public bool isHeld = false;
    public GameObject hitDirection;

    private bool damageEnabled = false;
    private float disableTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!damageEnabled || isHeld)
            return;

        disableTime += Time.deltaTime;
        if (disableTime > 2)
            damageEnabled = false;
    }

    public void Hold()
    {
        isHeld = true;
        damageEnabled = true;
        GetComponent<Rigidbody>().excludeLayers = ~(1 << 7);
    }
    
    public void Drop()
    {
        isHeld = false;
        disableTime = 0;
        GetComponent<Rigidbody>().excludeLayers = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!damageEnabled)
            return;
        HittableObject hittableObject = other.gameObject.GetComponent<HittableObject>();
        if (hittableObject == null)
            return;

        Vector3 dir = hitDirection.transform.forward.normalized;
        dir.y = 0;
        dir = dir.normalized;
        dir.y = 0.6f;

        if (hittableObject.GetType() == typeof(PowerUp))
        {
            ((PowerUp)hittableObject).Damage(damage, dir, this);
        }
        else
        {
            hittableObject.Damage(damage, dir, this);
        }
    }
}
