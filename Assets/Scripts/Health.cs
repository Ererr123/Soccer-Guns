using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    // Start is called before the first frame update
    int _currentHealth;
    int _currentMaxHealth;

    public int health
    {
        get
        {
            return _currentHealth;

        }
        set
        {
            _currentHealth = value;
        }
    }
    public int Maxhealth
    {
        get
        {
            return _currentMaxHealth;

        }
        set
        {
            _currentMaxHealth = value;
        }
    }

    public Health(int health, int maxHelath)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHelath;
    }

    public void DmgUnit(int dmgAmount)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
        }
    }
}
