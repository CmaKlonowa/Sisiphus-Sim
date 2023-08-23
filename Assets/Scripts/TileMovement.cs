using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    private BoxCollider boxCollider;

    public float downwardSpeed;
    public float zOffset;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        // set rotation to ground's
        transform.rotation = ground.transform.rotation;
        // determine start position
        transform.position = Vector3.zero;
        transform.Translate(Vector3.forward * boxCollider.size.z * 2 + new Vector3(0F,0F,zOffset));
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.back * downwardSpeed * Time.fixedDeltaTime);
        if (transform.localPosition.z - zOffset < 0F)
        {
            transform.Translate(Vector3.forward * boxCollider.size.z * 2);
        }
    }
}
