using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusProjectile : Projectile
{
    [SerializeField] private float minSpeed; 
    [SerializeField] private float maxSpeed;
    [SerializeField] private float lightningfrequency;
    private float speed;

    protected override void Spawn()
    {
        // get random speed value
        speed = Random.Range(minSpeed, maxSpeed);
        // Rotate in direction of the ground
        transform.rotation = ground.transform.rotation;
        transform.Rotate(Vector3.right, 0F);
        // Start throwing lightnings
        StartCoroutine("LightningCoroutine");

        base.Spawn();
    }

    protected override void PostSpawnBehaviour()
    {
        transform.position += ground.OkBackToAbsolute(Vector3.back * speed * Time.fixedDeltaTime);
    }

    [SerializeField] private GameObject lightning;
    IEnumerator LightningCoroutine()
    {
        while (!MainManager.IsGameOver)
        {
            yield return new WaitForSeconds(1/speed/lightningfrequency);
            var instance = Instantiate(lightning, transform.position, transform.rotation);
            instance.GetComponent<Lightning>().instantiator = gameObject;
        }
        
    }
}
