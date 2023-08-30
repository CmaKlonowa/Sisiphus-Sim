using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    // Just a method container 😂😁😁
    // A great example of ABSTRACTION

    private Plane thisPlane;
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

        thisPlane = new Plane(
            transform.rotation * Vector3.up,
            transform.position
        );
    }

    // ABSTRACTION
    public Vector3 ScreenToOnPlane(Vector2 screen, float depth = 0F)
    {
        Ray r = Camera.main.ScreenPointToRay(screen); // Direction pointed by the screen pos

        // Get and return the point where r hits the groundPlane
        if (thisPlane.Raycast(r, out float s))
        {
            Vector3 hitPoint = r.GetPoint(s);
            hitPoint += transform.rotation * Vector3.up * depth; // So that the player stays on the plane (instead of inside the plane)
            return hitPoint;
        }
        else
        {
            Debug.LogError("No mouse hit ;(");
            return Vector3.zero;
        }
    }
    
    // ABSTRACTION
    public Vector3 ToRealtiveToGround(Vector3 position)
    {
        return transform.InverseTransformPoint(position);
    }
    // ABSTRACTION
    public Vector3 OkBackToAbsolute(Vector3 relativePosition)
    {
        return transform.TransformPoint(relativePosition);
    }


}
