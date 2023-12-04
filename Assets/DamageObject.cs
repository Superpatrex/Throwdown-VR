using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DamageObject : MonoBehaviour
{
    public float damage = 1;
    public bool isHeld = false;
    public GameObject hitDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        HittableObject hittableObject = other.gameObject.GetComponent<HittableObject>();
        if (hittableObject == null)
            return;
        Vector3 dir = hitDirection.transform.forward.normalized;
        dir.y = 0;
        dir = dir.normalized;
        dir.y = 0.6f;
        hittableObject.Damage(damage, dir, this);
    }
}
