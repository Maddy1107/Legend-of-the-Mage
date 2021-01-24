using UnityEngine;
using UnityEngine.UI;

public class OneEyeAlienScript : MonoBehaviour
{
    public int OneEyeAlienmaxHealth = 100;

    public GameObject babyAlienPrefab;

    public GameObject firepoint;

    float nextspawntime;

    float nextpositionChangetime;

    EnemyWaveSpawner enemyWaveSpawner;

    public Slider health;

    public Image healthFill;

    public Gradient healthbargradient;

    public GameObject blast;

    private void Start()
    {
        enemyWaveSpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
    }
    private void Update()
    {
        if (OneEyeAlienmaxHealth == 0)
        {
            DestroyAllBabies();
            Destroy(gameObject);
            GameObject BlastPrefab = Instantiate(blast, transform.position, Quaternion.identity);
            Destroy(BlastPrefab, 2);
        }
        else if(nextspawntime < Time.time && OneEyeAlienmaxHealth >= 0)
        {
            SpawnBabies();
        }
        if(nextpositionChangetime < Time.time)
        {
            Transform randomspawnPoint = enemyWaveSpawner.spawnpoints[Random.Range(0, enemyWaveSpawner.spawnpoints.Length)];
            transform.position = randomspawnPoint.position;
            nextpositionChangetime = Time.time + 5;
        }
        health.value = OneEyeAlienmaxHealth;
        healthFill.color = healthbargradient.Evaluate(health.normalizedValue);
    }

    private void SpawnBabies()
    {
        GameObject baby = Instantiate(babyAlienPrefab, firepoint.transform.position, Quaternion.identity);
        enemyWaveSpawner.enemyScript = baby.GetComponent<EnemyScript>();
        nextspawntime = Time.time + 2;
    }

    void DestroyAllBabies()
    {
        
        GameObject[] babies = GameObject.FindGameObjectsWithTag("Enemy/OneEyeAlien");
        for (int i = 0; i < babies.Length; i++)
        {
                Destroy(babies[i]);
        }
    }
}
