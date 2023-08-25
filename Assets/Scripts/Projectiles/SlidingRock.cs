using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingRock : Projectile
{
    private TileMovement tile;

    protected override void Spawn()
    {
        tile = GameObject.Find("Tile").GetComponent<TileMovement>();
        base.Spawn();
    }

    protected override void PostSpawnBehaviour()
    {
        transform.position += tile.transform.rotation * Vector3.back * tile.downwardSpeed * Time.fixedDeltaTime;
    }
}
