using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Transform[] targets; // The object the bullets will target
    public float bulletSpeed = 10.0f;
    public float shootingInterval = 0.5f;

    private int currentTargetIndex = 0;
    private float elapsedTime = 0.0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Check if enough time has passed to shoot again
        if (elapsedTime >= shootingInterval)
        {
            Shoot();
            elapsedTime = 0.0f; // Reset the timer after shooting
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        if (bullet != null && targets.Length > 0)
        {
            // Get the current target
            Transform currentTarget = targets[currentTargetIndex];

            // Calculate direction to the current target
            Vector3 direction = (currentTarget.position - bulletSpawnPoint.position).normalized;

            // Set the bullet's velocity to move towards the current target
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

            // Increment the current target index or switch to the first target if the last one was destroyed
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }
}