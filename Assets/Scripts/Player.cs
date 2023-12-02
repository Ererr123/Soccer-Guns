using System.Collections;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textGoal;
    private StarterAssetsInputs starterAssetsInputs;
    public Animator animator;
    private Ball ballAttachedToPlayer;
    private float timeShot;
    public const int LAYER_SHOOT = 1;
    public int myScore,otherScore;
    private float goalTextColorAlpha;
    private AudioSource soundDribble;
    private AudioSource soundKick;
    private AudioSource soundCheer;
    private CharacterController controller;
    private float distanceSinceLastDribble;
    private float shootingPower;
    private float updateTime;

    private int currentLevel = 1;

    public Ball BallAttachedToPlayer { get => ballAttachedToPlayer; set => ballAttachedToPlayer = value; }
    public float ShootingPower { get => shootingPower; set => shootingPower = value; }
    // Start is called before the first frame update
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        soundDribble = GameObject.Find("Sound/dribble").GetComponent<AudioSource>();
        soundKick = GameObject.Find("Sound/kick").GetComponent<AudioSource>();
        soundCheer = GameObject.Find("Sound/cheer").GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (timeShot > 0)
        {
            if (ballAttachedToPlayer != null )
            {
                soundKick.Play();
                BallAttachedToPlayer.StickToPlayer = false;
                Rigidbody rigidbody = ballAttachedToPlayer.gameObject.GetComponent<Rigidbody>();
                Vector3 shootDirection = transform.forward;
                shootDirection.y += 0.2f;
                Debug.Log(shootingPower);
                rigidbody.AddForce(shootDirection *(4 + shootingPower *20f), ForceMode.Impulse);
                LooseBall();
            }

            if(Time.time - timeShot > 0.5)
            {
                timeShot = 0;
            }
        }
        else
        {
            animator.SetLayerWeight(LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(LAYER_SHOOT), 0f, Time.deltaTime * 10f));
        }

        if(goalTextColorAlpha > 0)
        {
            goalTextColorAlpha -= Time.deltaTime;
            textGoal.alpha = goalTextColorAlpha;
            textGoal.fontSize = 100 - (goalTextColorAlpha * 1 - 0);
        }
        if(ballAttachedToPlayer != null)
        {
            distanceSinceLastDribble += speed * Time.deltaTime;
            if(distanceSinceLastDribble > 3)
            {
                soundDribble.Play();
                distanceSinceLastDribble = 0;
            }
        }
    }
    public void LooseBall()
    {
        ballAttachedToPlayer = null;
        shootingPower = 0;
        Controller.Instance.RemovePowerBar();
    }
    public void Shoot()
    {
        if (ballAttachedToPlayer)
        {
            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", LAYER_SHOOT, 0f);
            animator.SetLayerWeight(LAYER_SHOOT, 1f);
        }
    }

    public void IncreaseMyScore()
    {
        myScore++;
        UpdateScore();

        // win condition
        if (myScore > 6)
        {
            currentLevel++;
            string nextScene = "Level" + currentLevel;

        // Check if the next level scene exists, otherwise, load a win screen
        if (SceneExists(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            LoadWinScreen();
        }
        }
    }

   private bool SceneExists(string sceneName)
   {
    int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
    return sceneIndex != -1 && sceneIndex < SceneManager.sceneCountInBuildSettings;
   }   

    
    public void LoadWinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }


    public void IncreaseOtherScore()
    {
        otherScore++;
        UpdateScore();
        if(otherScore > 6)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    private void UpdateScore()
    {
        soundCheer.Play();
        textScore.text = "Earth   " + myScore.ToString() + "|  Mars  " + otherScore.ToString();
        goalTextColorAlpha = 1f;

    }
}
