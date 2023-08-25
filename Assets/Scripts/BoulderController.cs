using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public Vector3 startPos;

    // Some variable declaration ((Look I hab encapculation :DDDD))
    [SerializeField] private float extraSpaceToWiggle;
    public float ExtraSpaceToWiggle
    {
        get { return extraSpaceToWiggle; }
        set {
            if (value < 0F) {
                Debug.LogError("You can't give the ball a negative space to wiggle 0_0"); // Yes you can't, that dosen't make sense
            }
            else {
                extraSpaceToWiggle = value;
            }
        }
    }
    
    private GroundController ground;
    private SphereCollider sphereCollider;
    void Start()
    {
        ground = GameObject.Find("Ground").GetComponent<GroundController>();
        sphereCollider = GetComponent<SphereCollider>();
        // set position to starting position
        transform.position = ground.OkBackToAbsolute(startPos);
    }

    void FixedUpdate()
    {
        // Constrain boulder's position
        ConstrainPosition();
    }

    // The boulder can't fly off the ground, that would be unfair! :(
    void ConstrainPosition()
    {
        // Get position relative to the ground
        Vector3 relativePos = ground.ToRealtiveToGround(transform.position);
        // Constrain the height of this relative position to the boulder's radius
        if (relativePos.y > sphereCollider.radius + ExtraSpaceToWiggle)//Also take into account that the boulder needs extra space to wiggle
        {
            relativePos = new Vector3(relativePos.x, sphereCollider.radius + ExtraSpaceToWiggle, relativePos.z);
        }
        // Convert it back to absolute position and set to bouler's transform
        transform.position = ground.OkBackToAbsolute(relativePos);
    }
}
