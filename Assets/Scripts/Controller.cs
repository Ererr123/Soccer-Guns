using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class Controller : MonoBehaviour
{
    public static Controller gameManger { get; private set; }

    public Health _playerHealth = new Health(100,100);

    Slider sliderPowerBar;
    GameObject powerBar;
    public static Controller Instance;
    private Ball scriptBall;
    private Player playerLastTouchedBall;
    private Player playerWithBall;
    private Player activeHumanPlayer;
    private Player passDestinationPlayer;
    private int teamWithBall;
    private int teamLastTouched;
    private int teamKickOff;
    private bool waitingForKickOff;
    private float waitingTimeKickOff;
    private float goalTextColorAlpha;
    // Start is called before the first frame update

    public void Awake()
    {
        if (gameManger != null && gameManger != this)
        {
            Destroy(this);
        }
        else
        {
            gameManger = this;
            Instance = this;
            sliderPowerBar = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
            powerBar = GameObject.Find("Canvas/Slider");
            powerBar.SetActive(false);
        }
    }

    public void SetPowerBar(float value)
    {
        powerBar.SetActive(true);
        sliderPowerBar.value = value;
    }

    public void RemovePowerBar()
    {
        powerBar.SetActive(false);
        sliderPowerBar.value = 0;
    }

}
