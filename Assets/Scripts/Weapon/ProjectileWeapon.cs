using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    public int damage = 10; // Sát thương của đạn
    public float projectileSpeed = 10f; // Tốc độ của đạn
    public float spawnInterval = 0.5f; // Thời gian giữa các lần bắn
    public float rotationSpeed = 180f; // Tốc độ xoay (độ/giây)

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tìm vị trí của kẻ thù gần nhất
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Tấn công kẻ thù gần nhất
            targetPosition = enemies[0].transform.position;
            // thêm 1 chút sai số
            targetPosition += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            for (int i = 1; i < enemies.Length; i++)
            {
                if (Vector2.Distance(transform.position, (Vector2)enemies[i].transform.position) < Vector2.Distance(transform.position, targetPosition))
                {
                    targetPosition = (Vector2)enemies[i].transform.position;
                }
            }
        }

        // Tính toán hướng di chuyển
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * projectileSpeed;

        // Xóa projectile sau 15 giây
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        // Xoay projectile mỗi khung hình
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    // override phương thức GetImage
    public override Sprite GetImage()
    {
        return transform.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Kiểm tra va chạm với kẻ thù
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Gọi phương thức TakeDamage trên kẻ thù
            }
        }
    }
}