using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // look at my
    // INHERITANCE
    public Vector3 gameArea;
    // ! Spawn pos is relative to ground !
    public Vector3 minSpawnPos;
    public Vector3 maxSpawnPos;
    protected static GroundController ground;
    // POLYMORPHISM
    // ABSTRACTION
    protected virtual void Spawn()
    {
        // determime spawn position relative to ground
        transform.position = ground.OkBackToAbsolute(
            VectorInRange(minSpawnPos, maxSpawnPos)
        );
    }
    // POLYMORPHISM
    // ABSTRACTION
    protected virtual void DestroyOutOfBounds()
    {
        // for every dimension, check if the projectile is too far
        for (int i = 0; i < 3; i++)
        {
            if (transform.position[i] > gameArea[i] || transform.position[i] < -gameArea[i])
            {
                // if yes, destroy
                Destroy(gameObject);
            }
        }
    }
    // POLYMORPHISM
    protected virtual void PostSpawnBehaviour()
    {
        // IDK 4 now😰
    }

    void Awake()
    {
        if (ground == null) { ground = GameObject.Find("Ground").GetComponent<GroundController>(); }

        Spawn();
    }

    void FixedUpdate()
    {
        DestroyOutOfBounds();
        PostSpawnBehaviour();
    }

    public Vector3 VectorInRange(Vector3 min, Vector3 max)
    {
        return new Vector3 (
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z) 
        );
    }
}
