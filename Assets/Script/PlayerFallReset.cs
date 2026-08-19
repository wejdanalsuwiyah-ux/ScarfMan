using UnityEngine;
using UnityEngine.SceneManagement; // مهم جداً لإعادة تحميل المشهد

public class PlayerFallReset : MonoBehaviour
{
    // يمكنك تعديل الارتفاع من الـ Inspector إذا أردت
    public float thresholdY = -2f;

    void Update()
    {
        // التحقق مما إذا كان موقع اللاعب على محور Y أقل من -2
        if (transform.position.y < thresholdY)
        {
            RestartRound();
        }
    }

    // دالة إعادة تشغيل المرحلة
    void RestartRound()
    {
        // إعادة تحميل المشهد الحالي بالكامل
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}