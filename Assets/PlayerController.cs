using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{


    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 10f;

    [Header("Jump / Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraTransform; // اسحب الكاميرا هنا من المحرر
    public float mouseSensitivity = 2f;
    public float minPitch = -60f;
    public float maxPitch = 80f;

    private CharacterController controller;
    private Vector3 velocity;
    private float pitch = 0f;
    private float targetYaw;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetYaw = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked; // قفل المؤشر لتحكم أفضل بالكاميرا
    }

    void Update()
    {
        HandleGroundCheck();
        HandleLook();
        HandleMovement();
        HandleJumpAndGravity();
    }

    void HandleGroundCheck()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // يبقيه ملتصقًا بالأرض
        }
    }

    void HandleLook()
    {
        if (cameraTransform == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // تحديث زاوية الدوران المطلوبة أفقياً
        targetYaw += mouseX;

        // دوران ناعم لجسم اللاعب نحو الزاوية المطلوبة (بدل الدوران المباشر)
        Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // تدوير الكاميرا عمودياً
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D
        float v = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 move = transform.right * h + transform.forward * v;
        move = Vector3.ClampMagnitude(move, 1f);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleJumpAndGravity()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
