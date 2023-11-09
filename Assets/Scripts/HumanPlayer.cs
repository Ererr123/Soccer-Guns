using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField] HealthBarScript _healthbar;
    Player scriptPlayer;
    private StarterAssetsInputs starterAssetsInputs;
    private CharacterController controller;
    public Animator animator;
    public bool movement = true;
    private float timeTackle;
    public InputAction playerControls;
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        scriptPlayer = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            timeTackle = Time.time;
            animator.Play("Tackled", 2, 0f);
            animator.SetLayerWeight(2, 2f);
            PlayerTakeDmg(10);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        Controller.gameManger._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(Controller.gameManger._playerHealth.health);
    }
}
