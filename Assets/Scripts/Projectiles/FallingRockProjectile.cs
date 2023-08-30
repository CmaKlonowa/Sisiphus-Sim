using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockProjectile : Projectile
{
    // look at my
    // INHERITANCE
    private Rigidbody thisRb;
    // POLYMORPHISM
    protected override void Spawn()
    {
        thisRb = GetComponent<Rigidbody>();
        base.Spawn();
    }


    // hit hard 😬
    public float hitKnockbackMultiplier;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Boulder"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(
                other.GetContact(0).normal * thisRb.velocity.magnitude * hitKnockbackMultiplier * -1, ForceMode.Impulse
            );
        }
    }
}
