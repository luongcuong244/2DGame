using UnityEngine;

public class SickleController : MonoBehaviour
{
    public int damage = 10; // Sát thương của kiếm
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