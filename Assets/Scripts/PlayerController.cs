using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GetTargetPosition settings
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject boulder;
    private Plane groundPlane;
    [SerializeField] private float width;

    [SerializeField] private float torqueMultilier;
    [SerializeField] private float forceMultilier;
    
    private Rigidbody playerRb;

    public float xRotation;
    public float rotationalSpeed;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // Convert plane GameObject to Plane object
        groundPlane = ToPlane(ground);
        // Start at the position of a mouse
        transform.position = GetTargetPosition();
    }

    void FixedUpdate()
    {
        // Go to the pointed position of a mouse in the way unity physics engine likes it
        playerRb.velocity = (GetTargetPosition() - transform.position) * forceMultilier;
        // Some rotation IDK what I did
        playerRb.MoveRotation(Quaternion.Euler(xRotation, 0F, ModularDistance(transform.rotation.z, 0F, 360F) * Time.fixedDeltaTime * rotationalSpeed));
    }

    // Outputs a point where the mouse points to the ground (IDK how to explain that better 😂)
    private Vector3 GetTargetPosition()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // Direction pointed by the mouse

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

    // Converts plane GameObject to Plane object
    private Plane ToPlane(GameObject obj)
    {
        return new Plane(
            obj.transform.rotation * Vector3.up,
            obj.transform.position
        );
    }

    // modulus is sus 😂😂🤣🤣🤣💀💀💀💀

    // Outputs the distance between a and b in modulo(modulus) number system
    private float ModularDistance(float a, float b, float modulus, bool sayIfCrossedModulus = false)
    {
        // calculate distance from a to b, without crossing the modulus
        float distance1 = Mathf.Abs(a - b);
        // calculate the distance when crossing modulus (distance from the smaller one to 0, and from bigger one to modulus)
        float distance2 = (a > b)? Mathf.Abs(a - modulus) + Mathf.Abs(b): Mathf.Abs(b - modulus) + Mathf.Abs(a);
        // compare them and output the smaller one
        // aditionally, distance2 is negated to indicate that it is crossing modulus. I think it makes sense since the line between a and b is going in the negative direction.
        return (distance1 < distance2)? distance1: ((sayIfCrossedModulus)? -1: 1) * distance2;
    }
}