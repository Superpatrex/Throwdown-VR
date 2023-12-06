using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour 
{
    public float movementSpeed = .4f * EnemyController.enemyMovementSpeedMultiplier;

    private GameObject head;
    private GameObject body;
    private GameObject left;
    private GameObject right;

    //public Rigidbody rb;

    private Vector3 headCenter;
    private Vector3 bodyCenter;
    private Vector3 leftCenter;
    private Vector3 rightCenter;

    public GameObject target;

    public NavMeshAgent agent;

    public Animator attackAnimation;

    public float minimumDistance = .5f;


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

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            Debug.Log("Add Rigid Body");
        }
        else if (GetComponent<Rigidbody>().isKinematic)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("ARemoved kinematic");
        }
        else if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
            Debug.Log("Add Box Collider");
        }

        //if (attackAnimation == null)
        //{
        //    attackAnimation = gameObject.GetComponent<Animator>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        MoveTowardsPlayer();
        //Jiggle();

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < this.minimumDistance)
        {
            //attackAnimation.SetTrigger("Attack");
            Debug.Log("Hit me Hit me");
        }
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
        agent.SetDestination(target.transform.position);

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
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
