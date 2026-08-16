using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // السيارة المتبعة
    public float distance = 5f; // المسافة بين الكاميرا والسيارة
    public float height = 2f; // ارتفاع الكاميرا بالنسبة للسيارة

    [Header("Camera Control")]
    public float smoothness = 10f; // سرعة تنعيم حركة الكاميرا
    public float rotationSpeed = 2f; // حساسية حركة الماوس

    [Header("Vertical Angles")]
    public float minVerticalAngle = -20f; // أدنى زاوية نظر لأسفل
    public float maxVerticalAngle = 60f;  // أعلى زاوية نظر لأعلى

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        // إخفاء مؤشر الماوس وقفله في منتصف الشاشة
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // قراءة زوايا الكاميرا الابتدائية
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;
    }

    void Update()
    {
        // قراءة حركة الماوس على محور X و Y
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // تقييد زاوية النظر العمودية لمنع انقلاب الكاميرا
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // حساب الدوران الكامل بناءً على المحورين X و Y
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // حساب موقع الكاميرا الجديد
        Vector3 targetOffset = new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + rotation * targetOffset;

        // تحريك الكاميرا بسلاسة للموقع الجديد
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);

        // توجيه نظر الكاميرا نحو الهدف
        transform.LookAt(target.position + Vector3.up * (height * 0.5f));
    }
}