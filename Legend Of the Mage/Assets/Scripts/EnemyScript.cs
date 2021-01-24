using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] EnemySpell;
    public GameObject EnemyFirepoint;

    Transform player;

    public float movespeed = 6f;
    public float enemyFirerate = 4f;
    float NextFire;

    Rigidbody2D rb;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameplay == true && GameManager.instance.gameOver == false)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5 && Time.time > NextFire)
                {
                    EnemyShoot();
                    NextFire = Time.time + enemyFirerate;
                }
                else if (Mathf.Abs(transform.position.x - player.transform.position.x) >= 5)
                {
                    EnemyMove();
                }
            }
        }
    }

    void EnemyShoot()
    {
        if(gameObject.tag == "Enemy/FireGolem")
        {
            Instantiate(EnemySpell[1], EnemyFirepoint.transform.position, Quaternion.identity);
        }
        else if(gameObject.tag == "Enemy/SpaceShip")
        {
            Instantiate(EnemySpell[2], EnemyFirepoint.transform.position, Quaternion.identity);
        }
        else if (gameObject.tag == "Enemy/OneEyeAlien")
        {
            Instantiate(EnemySpell[0], EnemyFirepoint.transform.position, Quaternion.identity);
        }
    }

    public void EnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);
    }
}
