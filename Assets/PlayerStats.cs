using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int health;
    public int attack;

    public void TakeDamage(int damage)
    {
        health -= damage;
        
    }


    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<PlayerStats>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }

}
