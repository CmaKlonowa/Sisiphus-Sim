using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GetTargetPosition settings
    [SerializeField] private GameObject ground;
    [SerializeField] private float width;

    
    [SerializeField] private float forceMultilier;
    
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerRb.velocity = (GetTargetPosition() - transform.position) * forceMultilier;
    }

    // Outputs a point where the mouse points to the ground (IDK how to explain that better 😂)
    private Vector3 GetTargetPosition()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // Direction pointed by the mouse
        
        // convert the ground game object to a plane
        Plane groundPlane = new Plane(
            ground.transform.rotation * Vector3.up,
            ground.transform.position
        );

        // Get and return the point where r hits the groundPlane
        if (groundPlane.Raycast(r, out float s))
        {
            Vector3 hitPoint = r.GetPoint(s);
            hitPoint += ground.transform.rotation * Vector3.up * width; // So that the player stays on the plane (instead of inside the plane)
            return hitPoint;
        }
        else
        {
            Debug.LogError("No mouse hit ;(");
            return Vector3.zero;
        }
    }
}