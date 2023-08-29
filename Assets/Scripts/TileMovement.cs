using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    public float downwardSpeed;
    public float zOffset;
    
    void Awake()
    {
        // set rotation to ground's
        transform.rotation = ground.transform.rotation;
        transform.Rotate(Vector3.up, 180F);
        // determine start position
        transform.position = Vector3.zero;
        transform.Translate(Vector3.back * transform.localScale.z * 5 + Vector3.back * zOffset);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * downwardSpeed * Time.fixedDeltaTime);
        if (transform.localPosition.z - zOffset < 0F)
        {
            transform.Translate(Vector3.back * transform.localScale.z * 5);
        }

        if (MainManager.IsGameOver)
        {
            // Stop Moving
            downwardSpeed = 0F;
        }
    }
}
