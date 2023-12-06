using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 0.4f;

    private GameObject head;
    private GameObject body;
    private GameObject left;
    private GameObject right;

    private Vector3 headCenter;
    private Vector3 bodyCenter;
    private Vector3 leftCenter;
    private Vector3 rightCenter;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        head = gameObject.GetNamedChild("Head");
        body = gameObject.GetNamedChild("Body");
        left = gameObject.GetNamedChild("Left");
        right = gameObject.GetNamedChild("Right");
        
        headCenter = head.transform.position;
        bodyCenter = body.transform.position;
        leftCenter = left.transform.position;
        rightCenter = right.transform.position;

        target = GameObject.Find("GorillaPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        //MoveTowardsPlayer();
        Jiggle();
    }

    void LookAtPlayer()
    {
        Vector3 relativePos = target.transform.position - transform.position;
        relativePos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 1);
    }

    void MoveTowardsPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 pos2 = target.transform.position;
        Vector3 diff = pos - pos2;
        diff.y = 0;
        diff = diff.normalized * -movementSpeed * Time.deltaTime;
        transform.position += diff;
    }

    void Jiggle()
    {
        Vector3 leftPos = leftCenter;
        leftPos.y = leftPos.y + 0.1f * Mathf.PerlinNoise(1, Time.timeSinceLevelLoad);
        left.transform.position = leftPos;

        Vector3 rightPos = rightCenter;
        rightPos.y = rightPos.y + 0.1f * Mathf.PerlinNoise(2, Time.timeSinceLevelLoad);
        right.transform.position = rightPos;

        Vector3 headPos = headCenter;
        headPos.x = headPos.x + 0.05f * Mathf.PerlinNoise(3, Time.timeSinceLevelLoad);
        headPos.y = headPos.y + 0.05f * Mathf.PerlinNoise(4, Time.timeSinceLevelLoad);
        headPos.z = headPos.z + 0.05f * Mathf.PerlinNoise(5, Time.timeSinceLevelLoad);
        head.transform.position = headPos;

        Vector3 bodyPos = bodyCenter;
        bodyPos.y = bodyPos.y + 0.1f * Mathf.PerlinNoise(6, Time.timeSinceLevelLoad);
        body.transform.position = bodyPos;
    }
}
