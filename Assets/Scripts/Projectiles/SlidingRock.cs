using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingRock : Projectile
{
    // look at my
    // INHERITANCE
    private TileMovement tile;

    public float scaleRange;
    public float rotationRange;

    protected override void Spawn()
    {
        tile = GameObject.Find("Tile").GetComponent<TileMovement>();

        // spawn with a random rotation and scale
        transform.rotation = GetRandomRotation();
        transform.localScale = GetRandomScale();
        // spawn as a random rock
        PickRandomChild();

        base.Spawn();
    }

    protected override void PostSpawnBehaviour()
    {
        transform.position += tile.transform.rotation * Vector3.forward * tile.downwardSpeed * Time.fixedDeltaTime;
    }

    private void PickRandomChild()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        // Make sure the rest isn't enabled
        foreach (var t in childTransforms)
        {
            if (t.gameObject != gameObject)// WHY, UNITY?
            {
                t.gameObject.SetActive(false);
            }
        }
        // Pick a random child and enable it
        childTransforms[Random.Range(0, childTransforms.Length)].gameObject.SetActive(true);
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(
            Random.Range(-rotationRange, rotationRange),
            Random.Range(-rotationRange, rotationRange),
            Random.Range(-rotationRange, rotationRange)
        );
    }

    private Vector3 GetRandomScale()
    {
        float scale = 1F + Random.Range(-scaleRange, +scaleRange);

        return new Vector3(scale, scale, scale);
    }
}
