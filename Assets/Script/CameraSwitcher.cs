using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;   // الكاميرا الأولى
    public Camera secondCamera; // الكاميرا الثانية

    public KeyCode switchKey = KeyCode.C; // الزر المستخدم للتبديل

    void Start()
    {
        // تفعيل الكاميرا الأولى وإيقاف الثانية عند بداية اللعبة
        if (mainCamera != null && secondCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            secondCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // عند الضغط على الزر المSpecified يتم العكس
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        if (mainCamera != null && secondCamera != null)
        {
            mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
            secondCamera.gameObject.SetActive(!secondCamera.gameObject.activeSelf);
        }
    }
}