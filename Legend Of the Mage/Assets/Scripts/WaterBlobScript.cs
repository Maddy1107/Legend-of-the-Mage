using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlobScript : MonoBehaviour
{
    Transform player;

    PlayerMovement playerMovement;

    public float movespeed = 5f;

    SpriteRenderer playerRender;

    public GameObject playerBlast;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRender = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.playerHealth -= 10;
            playerRender.color = new Color(1f, 0f, 0f);
            if (playerMovement.playerHealth == 0)
            {
                GameObject BlastPrefab = Instantiate(playerBlast, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(BlastPrefab, 2);
                Destroy(collision.gameObject);
                GameManager.instance.endgame();
            }

        }
        else if (collision.gameObject.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }
    }
}
