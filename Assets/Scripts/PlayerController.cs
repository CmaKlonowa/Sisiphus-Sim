using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GetTargetPosition settings
    private GroundController ground;
    [SerializeField] private GameObject boulder;
    [SerializeField] private float width;
    [SerializeField] private float torqueMultilier;
    [SerializeField] private float forceMultilier;
    private Rigidbody playerRb;
    public float xRotation;
    public float rotationalSpeed;
    public float xBounds;
    private Animator animator;
    private TileMovement tile;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        ground = GameObject.Find("Ground").GetComponent<GroundController>();
        animator = GetComponentsInChildren<Animator>()[0];
        tile = GameObject.Find("Tile").GetComponent<TileMovement>();
        // Start at the position of a mouse
        transform.position = ground.ScreenToOnPlane(Input.mousePosition, width);
    }

    void FixedUpdate()
    {
        // Go to the pointed position of a mouse in the way unity physics engine likes it
        playerRb.velocity = (MainManager.IsGameOver)? Vector3.zero : (ground.ScreenToOnPlane(Input.mousePosition, width) - transform.position) * forceMultilier;
        // Some rotation IDK what I did
        playerRb.MoveRotation(Quaternion.Euler(xRotation, 0F, ModularDistance(transform.rotation.z, 0F, 360F) * Time.fixedDeltaTime * rotationalSpeed));

        ConstrainPosition();
        ControllAnimation();
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

    void ConstrainPosition()
    {
        // dosen't accidentally no clip thu the plane
        Vector3 temp = ground.ToRealtiveToGround(transform.position);

        temp = new Vector3(temp.x, width, temp.z);

        // dosen't go off the plane
        if (temp.x > xBounds)
        {
            temp = new Vector3(xBounds, temp.y, temp.z);
        }
        else if (temp.x < -xBounds)
        {
            temp = new Vector3(-xBounds, temp.y, temp.z);
        }

        transform.position = ground.OkBackToAbsolute(temp);
    }

    void ControllAnimation()
    {
        // Logic for animations
        if (MainManager.IsGameOver)
        {
            animator.SetInteger("AnimID", 2);//die ;(
        }
        else if (ground.ToRealtiveToGround(playerRb.velocity).z < -tile.downwardSpeed)
        {
            animator.SetInteger("AnimID", 0);//don't go
        }
        else
        {
            animator.SetInteger("AnimID", 1);//ok u can go now :)
        }
    }
}