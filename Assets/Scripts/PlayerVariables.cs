using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public int startingHealth;
    int health;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncomingDamage(int value, string laserOwner)
    {
        health -= value;
        Debug.Log(health);
        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }
}
