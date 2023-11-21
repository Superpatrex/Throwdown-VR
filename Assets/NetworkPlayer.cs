using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] mestToDisable;

    public override void OnNetworkSpawn()
    {
        // Call the base functionality
        base.OnNetworkSpawn();

        // If the user is not the owner then we want to disable the mesh renderer
        // to avoid seeing the player twice
        if (IsOwner)
        {
            foreach (var mesh in mestToDisable)
            {
                mesh.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is the owner then we want to update the position of the player and rotation
        if (IsOwner)
        {
            // Update the position of the player
            root.position = VRRigReferences.Singleton.root.position;
            root.rotation = VRRigReferences.Singleton.root.rotation;

            // Update the position of the player's head
            head.position = VRRigReferences.Singleton.head.position;
            head.rotation = VRRigReferences.Singleton.head.rotation;

            // Update the position of the player's left hand
            leftHand.position = VRRigReferences.Singleton.leftHand.position;
            leftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;

            // Update the position of the player's right hand
            rightHand.position = VRRigReferences.Singleton.rightHand.position;
            rightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;
        }
    }
}
