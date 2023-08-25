using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float knockbackStrenght;
    public float speed;
    public float speedDecrease;
    private bool isFresh = true;

    public GameObject instantiator;

    private void FixedUpdate() {
        transform.Translate(Vector3.down * speed);
        speed -= speedDecrease * Time.fixedDeltaTime;

        if (!isFresh && speed > 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (isFresh)
        {
            isFresh = false;
            speed *= -1;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            KnockbackEverything();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Destroy(instantiator);
        }
    }

    private void KnockbackEverything()
    {
        // This function is heavy 🥶

        Rigidbody[] everything = Object.FindObjectsOfType<Rigidbody>();

        foreach (var thing in everything)
        {
            // Calculate Knockback strength based off distance
            Vector3 distanceVector = thing.position - transform.position;
            Vector3 knockbackVector = distanceVector.normalized * (1/distanceVector.sqrMagnitude) * knockbackStrenght;
            // Knockback!
            thing.AddForce(knockbackVector, ForceMode.Impulse);
        }
    }
}
