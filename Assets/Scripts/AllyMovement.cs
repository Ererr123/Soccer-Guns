using UnityEngine;

public class Shooting : MonoBehaviour
{
    /*
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Transform[] targets;
    public float bulletSpeed = 10.0f;
    public float shootingInterval = 0.5f;
    */

    public float moveSpeed = 5.0f;

    public Transform Player;

    // Define maximum boundaries
    public float maxLeft = -14f;
    public float maxRight = 14f;
    public float maxFront = 15f;
    public float maxBack = -23f;
    
    //private int currentTargetIndex = 0;
    private float elapsedTime = 0.0f;
    //private bool allEnemiesDestroyed = false;

    // getting ally animator
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        // Check if all enemies are destroyed
        if (!allEnemiesDestroyed)
        {
            CheckEnemiesDestroyed();
        }

        elapsedTime += Time.deltaTime;

        // Check if enough time has passed to shoot again and if all enemies are not destroyed
        if (elapsedTime >= shootingInterval && !allEnemiesDestroyed)
        {
            Shoot();
            elapsedTime = 0.0f; // Reset the timer after shooting
        }

        Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) ;
        transform.Translate(movement);

        // Clamp the position to stay within the defined boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, maxLeft, maxRight);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, maxBack, maxFront);
        transform.position = clampedPosition;


        */
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) ;
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        // movement
        if(movement != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            transform.rotation = Player.rotation;
    
        }
        else
        {
            animator.SetBool("IsMoving", false); // If there's no movement input, set isMoving to false
        }

        /*

        if (movement.magnitude > 0.01f)
        {
            animator.SetBool("IsMoving", true); // If there's movement input, set isMoving to true
            transform.Translate(movement);
            transform.forward = movement;


            // Clamp the position to stay within the defined boundaries
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, maxLeft, maxRight);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, maxBack, maxFront);
            transform.position = clampedPosition;
            
        }
        else
        {
            animator.SetBool("IsMoving", false); // If there's no movement input, set isMoving to false
        }
        */
    }

    /*
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        if (bullet != null && targets.Length > 0)
        {
            // Get the current target
            Transform currentTarget = targets[currentTargetIndex];

            if (currentTarget != null && currentTarget.gameObject.activeSelf)
            {
                // Calculate direction to the current target
                Vector3 direction = (currentTarget.position - bulletSpawnPoint.position).normalized;

                // Set the bullet's velocity to move towards the current target
                bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

                // logging shooting info
                Debug.Log(gameObject.name + " is shooting at " + currentTarget.name);

                // Increment the current target index or switch to the first target if the last one was destroyed
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            }
            else
            {
                // If the current target is destroyed or inactive, move to the next target
                MoveToNextTarget();
            }
        }
    }

    void MoveToNextTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            int nextIndex = (currentTargetIndex + i) % targets.Length;
            Transform potentialTarget = targets[nextIndex];
            if (potentialTarget != null && potentialTarget.gameObject.activeSelf)
            {
                currentTargetIndex = nextIndex;
                break;
            }
        }
    }

    void CheckEnemiesDestroyed()
    {
        allEnemiesDestroyed = true;
        foreach (Transform enemy in targets)
        {
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                allEnemiesDestroyed = false;
                break;
            }
        }

        if (allEnemiesDestroyed)
        {
            Debug.Log("All enemies destroyed. Stopping shooting.");
            // Optionally, perform any actions needed when all enemies are destroyed
        }
    }*/
}

