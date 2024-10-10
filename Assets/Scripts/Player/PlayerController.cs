using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public int damage = 10;
    public float damageSpeed = 1;
    public FixedJoystick fixedJoystick;
    public GameObject healthFill;
    public int health = 100;
    private int maxHealth = 100;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool isMovingLeft = true;
    private bool isHurting = false;
    private Coroutine attackCoroutine;


    private void Awake()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        if (fixedJoystick == null)
        {
            return;
        }
        movement = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
        if (movement.x > 0.1 || movement.x < -0.1) {
            isMovingLeft = movement.x < 0;
        }

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    private void AdjustPlayerFacingDirection()
    {
        if (isMovingLeft)
        {
            mySpriteRender.flipX = true;
        }
        else
        {
            mySpriteRender.flipX = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (!isHurting)
            {
                StartCoroutine(HurtCoroutine());
            }
        }
        updateHPBar();
    }

    private void updateHPBar()
    {
        float scale = (float)health / maxHealth;
        if (scale < 0)
        {
            scale = 0;
        }
        healthFill.transform.localScale = new Vector3(scale, 1, 1);
    }

    public void AddHP(int hp)
    {
        health += hp;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        updateHPBar();
    }

    private void Die()
    {
        // run animator to play die animation and destroy object after animation
        myAnimator.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }

    private IEnumerator HurtCoroutine()
    {
        isHurting = true;
        myAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.25f);
        isHurting = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            attackCoroutine = StartCoroutine(startAttack(other.gameObject.GetComponent<EnemyController>()));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StopCoroutine(attackCoroutine);
        }
    }

    private IEnumerator startAttack(EnemyController enemyController)
    {
        while (true)
        {
            enemyController.TakeDamage(damage);
            myAnimator.SetTrigger("Attack");
            yield return new WaitForSeconds(1/damageSpeed);
        }
    }

    public void AddNewSickle()
    {
        SickleManager sickleManager = GetComponentInChildren<SickleManager>();
        sickleManager.AddNewSickle();
    }

    public void IncreaseSicklesSpeed(int addSpeed) {
        SickleManager sickleManager = GetComponentInChildren<SickleManager>();
        sickleManager.IncreaseSicklesSpeed(addSpeed);
    }

    public void AddWeapon(BaseWeapon weapon)
    {
        WeaponManager weaponManager = GetComponentInChildren<WeaponManager>();
        weaponManager.AddWeapon(weapon);
    }

    public Sprite GetImage()
    {
        return transform.GetComponent<SpriteRenderer>().sprite;
    }
}