using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private float fps = 0.0f;
    private float fpsSmooth = 0.0f;
    private float smoothTime = 0.5f; // Thời gian làm mịn, thay đổi giá trị này nếu cần

    void LateUpdate()
    {
        // Tính toán deltaTime
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Tính FPS trung bình
        fps = 1.0f / deltaTime;

        // Làm mịn FPS (trung bình hóa)
        fpsSmooth = Mathf.Lerp(fpsSmooth, fps, Time.deltaTime / smoothTime);
    }

    void OnGUI()
    {
        // Hiển thị FPS mượt lên màn hình
        GUI.skin.label.fontSize = 20; // Đặt kích thước font
        GUI.color = Color.white; // Màu chữ
        GUI.Label(new Rect(10, 80, 100, 50), "FPS: " + Mathf.Round(fpsSmooth));
    }
}
