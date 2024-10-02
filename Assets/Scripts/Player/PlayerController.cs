using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    public FixedJoystick fixedJoystick;
    public GameObject healthFill;
    public int health = 100;
    private int maxHealth = 100;

    // private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool isMovingLeft = true;


    private void Awake()
    {
        maxHealth = health;
        // playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    // private void OnEnable()
    // {
    //     playerControls.Enable();
    // }

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
        //rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
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
            myAnimator.SetTrigger("Hurt");
            StartCoroutine(FlashCoroutine());
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

    private void Die()
    {
        // run animator to play die animation and destroy object after animation
        myAnimator.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
        GameManager.instance.GameOver();
    }

    private IEnumerator FlashCoroutine()
    {
        Color originalColor = mySpriteRender.color; // Lưu màu gốc
        Color flashColor = Color.red; // Màu nháy (có thể thay đổi)

        for (int i = 0; i < 2; i++)
        {
            mySpriteRender.color = flashColor; // Đặt màu nháy
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây
            mySpriteRender.color = originalColor; // Đặt lại màu gốc
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây
        }
    }
}

// // for on android
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 1f;

//     private PlayerControls playerControls;
//     private Vector2 movement;
//     private Rigidbody2D rb;
//     private Animator myAnimator;
//     private SpriteRenderer mySpriteRender;

//     private void Awake()
//     {
//         playerControls = new PlayerControls();
//         rb = GetComponent<Rigidbody2D>();
//         myAnimator = GetComponent<Animator>();
//         mySpriteRender = GetComponent<SpriteRenderer>();
//     }

//     private void OnEnable()
//     {
//         playerControls.Enable();
//     }

//     private void Update()
//     {
//         // Kiểm tra nhấn và giữ trên màn hình
//         HandleTouchInput();
//         // Gọi hàm để cập nhật animator
//         myAnimator.SetFloat("moveX", movement.x);
//         myAnimator.SetFloat("moveY", movement.y);
//     }

//     private void FixedUpdate()
//     {
//         Move();
//         AdjustPlayerFacingDirection();
//     }

//     private void HandleTouchInput()
//     {
//         // Kiểm tra nếu có chạm trên màn hình
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);
//             if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
//             {
//                 Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
//                 movement = (touchPosition - rb.position).normalized; // Tính hướng từ vị trí nhân vật đến vị trí chạm
//             }
//         }
//         else
//         {
//             movement = Vector2.zero; // Nếu không có chạm, dừng di chuyển
//         }
//     }

//     private void Move()
//     {
//         rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
//     }

//     private void AdjustPlayerFacingDirection()
//     {
//         if (movement.x < 0)
//         {
//             mySpriteRender.flipX = true;
//         }
//         else
//         {
//             mySpriteRender.flipX = false;
//         }
//     }
// }
