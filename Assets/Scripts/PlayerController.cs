using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool isMovingLeft = true;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
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
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        if (movement.x > 0.1 || movement.x < -0.1) {
            isMovingLeft = movement.x < 0;
        }

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
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
