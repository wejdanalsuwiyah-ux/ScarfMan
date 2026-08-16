using UnityEngine;

public class DriverCameraControl : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float sensitivity = 1.5f; // حساسية الماوس

    [Header("Horizontal Angles")]
    public float minAngle = -80f; // أقصى زاوية نظر لليسار
    public float maxAngle = 80f;  // أقصى زاوية نظر لليمين

    [Header("Weight & Smoothness")]
    public float smoothness = 3f; // ثقل الحركة (كلما قل الرقم أصبحت الكاميرا أثقل)

    private float targetYaw = 0f;
    private float currentYaw = 0f;

    void Start()
    {
        // قراءة الزاوية الابتدائية للكاميرا
        targetYaw = transform.localEulerAngles.y;
        currentYaw = targetYaw;
    }

    void Update()
    {
        // قراءة حركة الماوس الأفقية فقط
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        // إضافة المدخلات وإخضاعها للحدود الأفقية (يمين ويسار)
        targetYaw += mouseX;
        targetYaw = Mathf.Clamp(targetYaw, minAngle, maxAngle);

        // تنعيم الحركة لإعطاء الشعور بالثقل (Lerp)
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, Time.deltaTime * smoothness);

        // تطبيق الدوران على المحور Y فقط مع تثبيت باقي المحاور
        transform.localRotation = Quaternion.Euler(0f, currentYaw, 0f);
    }
}