using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;

    private void Update()
    {
       cam.forward = player.forward;
    }
}
