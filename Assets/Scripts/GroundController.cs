using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    
    public Vector3 ToRealtiveToGround(Vector3 position)
    {
        return transform.InverseTransformPoint(position);
    }
    public Vector3 OkBackToAbsolute(Vector3 relativePosition)
    {
        return transform.TransformPoint(relativePosition);
    }
}
