using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private float width;
    
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // test
        transform.position = GetTargetPosition();
    }

    // Outputs a point where the mouse hits the ground
    private Vector3 GetTargetPosition()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // Direction pointed by the mouse
        
        // convert the ground game object to a plane
        Plane groundPlane = new Plane(
            ground.transform.rotation * Vector3.up,
            ground.transform.position
        );

        // Get the point where r hits the groundPlane
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