using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField] HealthBarScript _healthbar;
    Player scriptPlayer;

    [SerializeField]
    private GameObject bulletPrefab;


    [SerializeField]
    private Transform attackPosition;

    [SerializeField]
    private Transform bulletparent;
    [SerializeField] private float rotationSpeed = 5f;
    private InputAction gunAction;
    private PlayerInput playerInput;
    private Transform CameraTransform;
    private StarterAssetsInputs starterAssetsInputs;
    private CharacterController controller;
    public Animator animator;
    public bool movement = true;
    public LayerMask IgnoreMe;
    private float timeTackle;
    private float timeShot;
    public InputAction playerControls;
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        scriptPlayer = GetComponent<Player>();
        CameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        gunAction = playerInput.actions["Gun"];
    }

    private void OnEnable()
    {

        gunAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        gunAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        animator.SetLayerWeight(3, 1);
        timeShot = Time.time;
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity, bulletparent);
        AllyBulletController bulletController = bullet.GetComponent<AllyBulletController>();
        if (Physics.Raycast(CameraTransform.position,CameraTransform.forward, out hit, 300f, ~IgnoreMe))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = CameraTransform.position+CameraTransform.forward *25f;
            bulletController.hit = false;
        }
    }

    void Update()
    {   
        if(Time.time - timeShot > .75)
        {
            timeShot = 0;
            animator.SetLayerWeight(3, 0);
        }
        if (Time.time - timeTackle >2.2)
        {
            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
        }
        if (starterAssetsInputs.shoot){
            if (scriptPlayer.BallAttachedToPlayer)
            {
                scriptPlayer.ShootingPower += 1.5f * Time.deltaTime;
                Controller.Instance.SetPowerBar(scriptPlayer.ShootingPower);
                if (scriptPlayer.ShootingPower > 1)
                {
                    scriptPlayer.ShootingPower = 1;
                }
            }
        }
        else if (scriptPlayer.ShootingPower>0)
        {
            scriptPlayer.Shoot();
        }
        RotatePlayerToCameraDirection();
    }
    private void RotatePlayerToCameraDirection()
    {
        Vector3 cameraForward = CameraTransform.forward;
        cameraForward.y = 0; // Ensure rotation only happens on the Y axis
        if (cameraForward.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Runner"))
        {
            timeTackle = Time.time;
            animator.Play("Tackled", 2, 0f);
            animator.SetLayerWeight(2, 2f);
            PlayerTakeDmg(10);
        }

        if (other.CompareTag("EnemyBullet"))
        {
            PlayerTakeDmg(20);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        Controller.gameManger._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(Controller.gameManger._playerHealth.health);
        if (Controller.gameManger._playerHealth.health <= 0)
        {
            // Load the intro screen scene
            SceneManager.LoadScene("MainMenu");
        
        }
    }
}
