using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cap: MonoBehaviour
{
    [SerializeField] public float helath, maxHealth;
    [SerializeField] EnemyHealthScript healthbar;
    [SerializeField] public int type;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("AllyBullet")))
        {
            PlayerTakeDmg(20);
        }
    }

    private void PlayerTakeDmg(float dmg)
    {
        helath -= dmg;
        healthbar.UpdateHealthBar(helath, maxHealth);
    }
}
