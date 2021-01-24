using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellShootScript : MonoBehaviour
{
    public GameObject[] blast = new GameObject[2];

    public float speed = 0.1f;

    Vector3 moveDirection;

    OneEyeAlienScript OneEyeAlienScript;

    public GameObject babyAlienPrefab;

    public GameObject enemyHitPrefab;

    PlayerMovement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Enemy/OneEyeAlienMax") != null)
        {
            OneEyeAlienScript = GameObject.FindGameObjectWithTag("Enemy/OneEyeAlienMax").GetComponent<OneEyeAlienScript>();
        }
        playermovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        CalculateDirectiontoMouse();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = moveDirection;
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
        Destroy(gameObject, 2f);
    }

    public void CalculateDirectiontoMouse()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy/WaterBlob") && gameObject.CompareTag("PlayerSpell/FireSpell"))
        {
            DestroyEnemyAndSpell(collision);
            GameObject BlastPrefab = Instantiate(blast[1], collision.gameObject.transform.position, Quaternion.identity);
            Destroy(BlastPrefab, 2);
            FindObjectOfType<AudioManager>().Play("Explosion");
            if(playermovement.playerHealth != 100)
                playermovement.playerHealth += 10;
        }
        else if (collision.gameObject.CompareTag("Enemy/SpaceShip") && gameObject.CompareTag("PlayerSpell/ElectricSpell"))
        {
            DestroyEnemyAndSpell(collision);
            GameObject BlastPrefab = Instantiate(blast[0], collision.gameObject.transform.position, Quaternion.identity);
            Destroy(BlastPrefab, 2);
            FindObjectOfType<AudioManager>().Play("Explosion");
            if (playermovement.playerHealth != 100)
                playermovement.playerHealth += 10;
        }
        else if (collision.gameObject.CompareTag("Enemy/FireGolem") && gameObject.CompareTag("PlayerSpell/WaterSpell"))
        {
            DestroyEnemyAndSpell(collision);
            GameObject BlastPrefab = Instantiate(blast[1], collision.gameObject.transform.position, Quaternion.identity);
            Destroy(BlastPrefab, 2);
            FindObjectOfType<AudioManager>().Play("Explosion");
            if (playermovement.playerHealth != 100)
                playermovement.playerHealth += 10;
        }
        else if (collision.gameObject.CompareTag("Enemy/OneEyeAlien") && gameObject.CompareTag("PlayerSpell/Poison"))
        {
            DestroyEnemyAndSpell(collision);
            GameObject BlastPrefab = Instantiate(blast[2], collision.gameObject.transform.position, Quaternion.identity);
            Destroy(BlastPrefab, 2);
            if (playermovement.playerHealth != 100)
                playermovement.playerHealth += 10;
            FindObjectOfType<AudioManager>().Play("Explosion");
        }
        else if (collision.gameObject.CompareTag("Enemy/OneEyeAlienMax") && gameObject.CompareTag("PlayerSpell/Poison"))
        {
            OneEyeAlienScript.OneEyeAlienmaxHealth -= 10;
            GameObject EnemyHit = Instantiate(enemyHitPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(EnemyHit, 2);
            Destroy(gameObject);
        }
    }

    void DestroyEnemyAndSpell(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
