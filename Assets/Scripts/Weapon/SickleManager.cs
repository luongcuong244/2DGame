using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleManager : MonoBehaviour
{
    public GameObject sicklePrefab; // Prefab của kiếm
    public float rotationSpeed = 10f; // Tốc độ xoay của kiếm
    public float radius = 3f; // Bán kính xoay quanh nhân vật
    public int damage = 10; // Sát thương của kiếm
    public int sickleCount = 1; // Số lượng kiếm ban đầu

    private List<GameObject> sickles = new List<GameObject>(); // mảng chứa các kiếm
    private float currentRotation; // Để theo dõi góc hiện tại

    private void Start()
    {
        // Khởi tạo 1 lưỡi liềm ban đầu
        SetupSickles(sickleCount);
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

    public void SetupSickles(int count)
    {
        // Xóa các kiếm cũ
        foreach (GameObject sickle in sickles)
        {
            Destroy(sickle);
        }
        sickles.Clear();

        // Tính góc giữa các kiếm
        float angleIncrement = 360f / count;

        for (int i = 0; i < count; i++)
        {
            // Tạo kiếm mới
            AddSickle();

            // Tính toán vị trí cho kiếm mới
            float angle = angleIncrement * i;
            Vector3 offset = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            sickles[i].transform.localPosition = offset; // Đặt vị trí kiếm
        }
    }

    public void AddNewSickle()
    {
        // Thêm một lưỡi liềm mới
        int newCount = sickles.Count + 1;
        SetupSickles(newCount);
    }

    public void IncreaseSicklesSpeed(int addSpeed)
    {
        // Tăng tốc độ xoay của lưỡi liềm
        rotationSpeed += addSpeed;
    }

    private void Update()
    {
        // Xoay quanh nhân vật
        currentRotation += rotationSpeed * Time.deltaTime;

        // Cập nhật vị trí và xoay của từng lưỡi liềm
        for (int i = 0; i < sickles.Count; i++)
        {
            float angle = (360f / sickles.Count) * i + currentRotation;
            Vector3 offset = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            sickles[i].transform.localPosition = offset; // Cập nhật vị trí kiếm
            sickles[i].transform.rotation = Quaternion.Euler(0, 0, angle); // Xoay kiếm
        }
    }
}