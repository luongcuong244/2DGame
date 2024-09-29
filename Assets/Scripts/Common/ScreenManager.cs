using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Stack dùng để lưu trữ các màn hình
    private static Stack<GameObject> screenStack = new Stack<GameObject>();

    // Danh sách tất cả các màn hình (Canvas hoặc Panel)
    public List<GameObject> screens;

    // Màn hình khởi tạo
    public GameObject initialScreen;

    private void Start()
    {
        // Ẩn tất cả các màn hình khi bắt đầu
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        // Hiển thị màn hình khởi tạo
        if (initialScreen != null)
        {
            Push(initialScreen.name);
        }
    }

    // Phương thức static Push để thêm màn hình mới bằng tên
    public void Push(string screenName)
    {
        // Tìm màn hình dựa trên tên
        GameObject newScreen = screens.Find(screen => screen.name == screenName);

        // Nếu màn hình không tìm thấy, trả về
        if (newScreen == null)
        {
            Debug.LogWarning($"Screen '{screenName}' not found!");
            return;
        }

        // Nếu có màn hình hiện tại, ẩn nó đi
        if (screenStack.Count > 0)
        {
            GameObject currentScreen = screenStack.Peek();
            currentScreen.SetActive(false);
        }

        // Hiển thị màn hình mới
        newScreen.SetActive(true);

        // Đưa màn hình mới vào stack
        screenStack.Push(newScreen);
    }

    // Phương thức static Pop để quay lại màn hình trước đó
    public void Pop()
    {
        // Nếu không có màn hình nào trong stack, thoát ra
        if (screenStack.Count == 0)
        {
            Debug.LogWarning("No screens to pop!");
            return;
        }

        // Ẩn màn hình hiện tại
        GameObject currentScreen = screenStack.Pop();
        currentScreen.SetActive(false);

        // Nếu còn màn hình trước đó trong stack, hiển thị nó
        if (screenStack.Count > 0)
        {
            GameObject previousScreen = screenStack.Peek();
            previousScreen.SetActive(true);
        }
    }

    // Phương thức static ShowScreen để hiển thị màn hình cụ thể
    public void ShowScreen(string screenName)
    {
        // Tìm màn hình dựa trên tên và in ra
        screens.ForEach(screen => Debug.Log(screen.name));
        GameObject screen = screens.Find(screen => screen.name == screenName);

        // Nếu màn hình không tìm thấy, trả về
        if (screen == null)
        {
            Debug.LogWarning($"Screen '{screenName}' not found!");
            return;
        }

        // Nếu có màn hình hiện tại, ẩn nó đi
        if (screenStack.Count > 0)
        {
            GameObject currentScreen = screenStack.Peek();
            currentScreen.SetActive(false);
        }

        // Hiển thị màn hình được chỉ định
        screen.SetActive(true);

        // Đưa màn hình đó vào stack
        screenStack.Push(screen);
    }
}