using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 25f;
    private float timeToDestroy = 5;

    public Vector3 target {  get;  set; }
    public bool hit { get; set; }

    public void OnEnable()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position,target) < .01f)
        {
            //Destroy(this.gameObject);
        }
    }


}
