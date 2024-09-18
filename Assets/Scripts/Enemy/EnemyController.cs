using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Tham chiếu đến transform của người chơi
    public float moveSpeed = 3f; // Tốc độ di chuyển của kẻ thù
    public GameObject healthFill;
    public int health = 100;
    private int maxHealth = 100;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>(); // Lấy Animator
        mySpriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
    }

    private void Update()
    {
        MoveTowardsPlayer();
        FlipTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Tính toán hướng di chuyển
            Vector2 direction = (player.position - transform.position).normalized;

            // Cập nhật vị trí của kẻ thù
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    private void FlipTowardsPlayer()
    {
        if (player != null)
        {
            // Kiểm tra vị trí của người chơi so với kẻ thù
            if (player.position.x < transform.position.x)
            {
                // Nếu người chơi ở bên trái, flip kẻ thù
                mySpriteRenderer.flipX = true;
            }
            else
            {
                // Nếu người chơi ở bên phải, không flip kẻ thù
                mySpriteRenderer.flipX = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        } else {
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
        Destroy(gameObject, 0.25f);
    }

    private IEnumerator FlashCoroutine()
    {
        Color originalColor = mySpriteRenderer.color; // Lưu màu gốc
        Color flashColor = Color.red; // Màu nháy (có thể thay đổi)

        for (int i = 0; i < 2; i++)
        {
            mySpriteRenderer.color = flashColor; // Đặt màu nháy
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây
            mySpriteRenderer.color = originalColor; // Đặt lại màu gốc
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây
        }
    }

    public void setPlayer(Transform player)
    {
        this.player = player;
    }
}