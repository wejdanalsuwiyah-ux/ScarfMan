using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{

    public int points = 10;
    public float rotationSpeed = 100f;

    void Update()
    {
        // انيميشن دوران الكوين
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreCounter.AddScore(points);
            Destroy(gameObject);
        }
    }
}