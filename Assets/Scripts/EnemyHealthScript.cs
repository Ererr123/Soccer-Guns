using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] public Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
