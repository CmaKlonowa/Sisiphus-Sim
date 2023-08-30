using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject[] projectiles;
    public static bool IsGameOver;
    public float spawnTimeRange;

    public float[] projectileCooldownTimes;

    public AudioSource mainAudioSource;
    
    void Awake()
    {
        mainAudioSource.volume = GameManager.volume;
        projectileCooldownTimes = new float[projectiles.Length]; 

        for (int i = 0; i < projectiles.Length; i++)
        {
            // Initial cooldown = (i+2)^2
            projectileCooldownTimes[i] = (float)((i+2)*(i+2));
            StartCoroutine("ProjectileSpawningCoroutine", i);
        }
    }

    IEnumerator ProjectileSpawningCoroutine(int id)
    {
        while (!IsGameOver)
        {
            // Wait the cooldown, randomize it a bit
            yield return new WaitForSeconds(projectileCooldownTimes[id] + Random.Range(-spawnTimeRange, spawnTimeRange));
            // Lower projectile cooldown a bit
            projectileCooldownTimes[id] *= 0.9F;
            if (projectileCooldownTimes[id] < id+1) {
                projectileCooldownTimes[id] = (float)id+1;
            }
            // Instantiate the Projectile
            Instantiate(projectiles[id], projectiles[id].transform.position, projectiles[id].transform.rotation);
        }
        
        
    }
    
}
