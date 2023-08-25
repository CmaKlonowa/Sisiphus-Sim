using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbProjectile : Projectile
{
    private Rigidbody thisRb;
    private GameObject myFocus; //the birb's main focus is boulder

    public float flyForceStrength;
    public float hitStrength;

    private bool isDie = false;

    //private Quaternion rotationOffset;
    protected override void Spawn()
    {
        thisRb = GetComponent<Rigidbody>();
        //rotationOffset = transform.rotation;
        myFocus = GameObject.Find("Boulder");

        base.Spawn();

        // negate x at random
        transform.position = new Vector3(
            transform.position.x * (1-Random.Range(0,2)*2),
            transform.position.y,
            transform.position.z
        );
    }

    protected override void PostSpawnBehaviour()
    {
        thisRb.AddForce(GetFocusedForce() * flyForceStrength * Time.fixedDeltaTime);
        transform.rotation = GetFocusedRotation();
    }

    private void OnCollisionEnter(Collision other) {
        if (!isDie)
        {
            if (other.gameObject.CompareTag("Boulder"))
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(GetFocusedForce().normalized * hitStrength, ForceMode.Impulse);
                Die();
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Die();
            }
        } 
    }

    private void Die()
    {
        thisRb.useGravity = true;
        isDie = true;
    }

    private Quaternion GetFocusedRotation()
    {
        Vector3 focus = (isDie)? thisRb.velocity: GetFocusedForce();
        return Quaternion.LookRotation(focus);
    }

    private Vector3 GetFocusedForce()
    {
        return (myFocus == null)? Vector3.zero : myFocus.transform.position - transform.position;
    }
}
