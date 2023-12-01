using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCameraRotate : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
       transform.LookAt(target.position);

        
    }
}
