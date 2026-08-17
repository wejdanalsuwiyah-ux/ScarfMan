using UnityEngine;

public class VehicleEnterExit : MonoBehaviour
{
    public GameObject player;
    public MonoBehaviour playerMovementScript;
    public MonoBehaviour carMovementScript; // سكربت حركة السيارة
    public Transform exitPoint;

    [Header("Car Physics")]
    public Rigidbody carRigidbody; // اسحب Rigidbody السيارة هنا

    [Header("Cameras")]
    public GameObject playerCamera; // كاميرا اللاعب
    public GameObject carCamera;    // كاميرا السيارة

    private bool isInCar = false;
    public bool isNearCar = false;

    void Start()
    {
        // إيقاف حركة السيارة وكاميرتها في بداية اللعبة
        if (carCamera != null) carCamera.SetActive(false);

        DisableCarMovement();
    }

    void Update()
    {
        // دخول السيارة بزر Q
        if (isNearCar && !isInCar && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("ss");
            Debug.Log("ss");
            EnterCar();
        }
        // خروج من السيارة بزر Space
        else if (isInCar && Input.GetKeyDown(KeyCode.Space))
        {
            ExitCar();
        }
    }

    void EnterCar()
    {
        isInCar = true;

        // إخفاء اللاعب وتعطيل حركته
        player.SetActive(false);
        playerMovementScript.enabled = false;

        // تفعيل حركة السيارة والفيزياء
        EnableCarMovement();

        // تبديل الكاميرات
        if (playerCamera != null) playerCamera.SetActive(false);
        if (carCamera != null) carCamera.SetActive(true);
    }

    void ExitCar()
    {
        isInCar = false;

        // نقل اللاعب لنقطة الخروج وإظهاره
        player.transform.position = exitPoint.position;
        player.SetActive(true);
        playerMovementScript.enabled = true;

        // إيقاف حركة السيارة والفيزياء تماماً
        DisableCarMovement();

        // تبديل الكاميرات
        if (carCamera != null) carCamera.SetActive(false);
        if (playerCamera != null) playerCamera.SetActive(true);
    }

    void EnableCarMovement()
    {
        // تفعيل سكربت حركة السيارة
        if (carMovementScript != null)
            carMovementScript.enabled = true;

        // تفعيل الفيزياء للسيارة
        if (carRigidbody != null)
        {
            carRigidbody.isKinematic = false;
        }
    }

    void DisableCarMovement()
    {
        // تعطيل سكربت حركة السيارة
        if (carMovementScript != null)
            carMovementScript.enabled = false;

        // تصفير سرعة السيارة وإيقاف فيزيائيتها
        if (carRigidbody != null)
        {
            carRigidbody.linearVelocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
            carRigidbody.isKinematic = true; // لمنع أي حركة أو تأثير فيزيائي
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isNearCar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isNearCar = false;
    }
}