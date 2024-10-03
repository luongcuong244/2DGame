using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleManager : MonoBehaviour
{
    public GameObject sicklePrefab; // Prefab của kiếm
    public float rotationSpeed = 100f; // Tốc độ xoay của kiếm
    public float radius = 3f; // Bán kính xoay quanh nhân vật
    public int damage = 20; // Sát thương của kiếm

    private Vector3 offset; // Vị trí ban đầu của kiếm

    // mảng chứa các kiếm
    private List<GameObject> sickles = new List<GameObject>();

    private void Start()
    {
        // Đặt kiếm ở vị trí ban đầu dựa vào bán kính
        offset = new Vector3(radius, 0, 0);
        transform.localPosition = offset; // Đặt kiếm ở vị trí bán kính
    }

    private void AddSickle()
    {
        // Tạo một kiếm mới
        GameObject sickle = Instantiate(sicklePrefab, transform.position, Quaternion.identity);
        sickle.transform.SetParent(transform); // Đặt kiếm làm con của nhân vật
        SickleController sickleController = sickle.GetComponent<SickleController>();
        sickleController.damage = damage; // Đặt sát thương cho kiếm
        sickles.Add(sickle); // Thêm kiếm vào mảng
    }

    // khởi tạo vị trí của các kiếm để tạo hiệu ứng xoay quanh nhân vật
    public void SetupSickles(int count)
    {
        // Xóa các kiếm cũ
        foreach (GameObject sickle in sickles)
        {
            Destroy(sickle);
        }
        sickles.Clear();

        // Tính góc giữa các kiếm
        float angle = 360f / count;
        for (int i = 0; i < count; i++)
        {
            // Tạo kiếm mới
            AddSickle();
            // Xoay kiếm
            offset = Quaternion.Euler(0, 0, angle) * offset;
        }
    }

    private void Update()
    {
        // Xoay kiếm quanh nhân vật
        float angle = rotationSpeed * Time.deltaTime;
        offset = Quaternion.Euler(0, 0, angle) * offset; // Tính toán vị trí mới
        transform.localPosition = offset; // Cập nhật vị trí kiếm
        transform.Rotate(Vector3.forward, angle); // Xoay kiếm
    }
}
