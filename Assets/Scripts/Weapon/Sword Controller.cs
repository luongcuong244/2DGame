using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Tốc độ xoay của kiếm
    public float radius = 3f; // Bán kính xoay quanh nhân vật
    public int damage = 20; // Sát thương của kiếm

    private Vector3 offset; // Vị trí ban đầu của kiếm

    private void Start()
    {
        // Đặt kiếm ở vị trí ban đầu dựa vào bán kính
        offset = new Vector3(radius, 0, 0);
        transform.localPosition = offset; // Đặt kiếm ở vị trí bán kính
    }

    private void Update()
    {
        // Xoay kiếm quanh nhân vật
        float angle = rotationSpeed * Time.deltaTime;
        offset = Quaternion.Euler(0, 0, angle) * offset; // Tính toán vị trí mới
        transform.localPosition = offset; // Cập nhật vị trí kiếm
        transform.Rotate(Vector3.forward, angle); // Xoay kiếm
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