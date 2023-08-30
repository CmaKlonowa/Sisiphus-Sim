using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public Vector3 startPos;
    private Rigidbody thisRb;
    private float[] tooFarYPositions;

    // ENCAPSULATION
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
        thisRb = GetComponent<Rigidbody>();
        // set position to starting position
        transform.position = ground.OkBackToAbsolute(startPos);

        // Calculate too far low and high position
        tooFarYPositions = new float[2];

        var canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        Vector3[] edges = new Vector3[4];
        canvasRect.GetWorldCorners(edges);

        for (int i = 0; i < 2; i++)
        {
            tooFarYPositions[i] = ground.ScreenToOnPlane(edges[i * 2]).y;
        }
        
    }

    void FixedUpdate()
    {
        // Constrain boulder's position
        ConstrainPosition();
        // Drag if too far
        DragIfTooFar();

        ManageGameOver();
    }

    // ABSTRACTION
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

    // ABSTRACTION
    public float tooFarDrag;
    void DragIfTooFar()
    {
        thisRb.drag = (transform.position.y > tooFarYPositions[1]+extraSpaceToWiggle && thisRb.velocity.y > 0)? tooFarDrag: 0F;
    }

    // ABSTRACTION
    // If boulder is too far low, one must imagine Sisyphus happy
    void ManageGameOver()
    {
        if (transform.position.y < tooFarYPositions[0]-extraSpaceToWiggle)
        {
            GameObject.Find("MainManager").GetComponent<MainUIManager>().EndGame(true);
        }
    }
}
