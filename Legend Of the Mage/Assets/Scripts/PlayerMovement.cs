using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject firepoint;

    public GameObject[] PlayerSpell = new GameObject[3];

    Animator SpellShootAnimation;

    public Image CooldownImage;

    Rigidbody2D rb;

    SpriteRenderer playerRender;

    public Slider health;

    public Image healthFill;

    public Gradient healthbargradient;

    Vector3 Direction;

    float speed = 10;
    float firerate = 5f;
    float Horizontalx;
    float Verticalx;
    float OnhitColorChangeTime;

    public int playerHealth = 100;
    public int playerSpellIndex;

    bool isCooldown = false;
    public int cooldownTime = 1;
    

    private void Start()
    {
        SpellShootAnimation = GetComponent<Animator>();
        CooldownImage.fillAmount = 0;
        rb = GetComponent<Rigidbody2D>();
        playerRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.instance.gameplay == true)
        {
            CalculateMousePositionToShoot();

            Vector3 Spritescale = transform.localScale;

            Horizontalx = Input.GetAxis("Horizontal");
            Verticalx = Input.GetAxis("Vertical");

            if (Direction.x < 0)
            {
                Spritescale.x = 1;
            }
            else if (Direction.x > 0)
            {
                Spritescale.x = -1;
            }

            transform.localScale = Spritescale;

            if (Input.GetMouseButtonDown(0) && isCooldown == false)
            {
                SpellShootAnimation.SetTrigger("Shoot");
                Shoot();
                CooldownImage.fillAmount = 0;
                isCooldown = true;
            }

            if (isCooldown)
            {
                CooldownImage.fillAmount += cooldownTime / firerate * Time.deltaTime;
                if (CooldownImage.fillAmount >= 1)
                {
                    isCooldown = false;
                }
            }

            health.value = playerHealth;
            healthFill.color = healthbargradient.Evaluate(health.normalizedValue);

            if (OnhitColorChangeTime < Time.time)
            {
                playerRender.color = new Color(1f, 1f, 1f);
                OnhitColorChangeTime = 0.1f + Time.time;
            }
        }
    }

    private void CalculateMousePositionToShoot()
    {
        Direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Direction.z = 0;
        Direction.Normalize();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(Horizontalx * speed * Time.deltaTime, Verticalx * speed * Time.deltaTime)) ;
    }

    public void Shoot()
    {
        Instantiate(PlayerSpell[playerSpellIndex], firepoint.transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Shoot");
    }
}
