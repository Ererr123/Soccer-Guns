using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Point from which bullets will be spawned
    public Transform enemy; // Reference to the enemy
    public float shootInterval = 2f; // Time interval between shots
    public float bulletSpeed = 10f; // Speed of the bullets
    private bool canShoot = true;

    private void Start()
    {
        StartCoroutine(ShootAtInterval());
    }

    IEnumerator ShootAtInterval()
    {
        while (true)
        {
            if (canShoot)
            {
                Shoot();
                yield return new WaitForSeconds(shootInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Adjust bullet direction towards the enemy
        Vector3 direction = (enemy.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed; // Bullet speed adjusted here
    }
}