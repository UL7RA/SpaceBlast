using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviourPun
{
    public int startingHealth;
    public int health;
    public int score;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        if (photonView.IsMine)
        {
            healthBar.maxValue = startingHealth;
            healthBar.value = startingHealth;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void IncomingDamage(int value, Player laserOwner)
    {
        if (photonView.IsMine)
        {
            health -= value;
            healthBar.DOValue(health, 0.5f);
            Debug.Log(health);
            if (health <= 0)
            {
                photonView.RPC("AddScore", laserOwner);
                KillPlayer();
            }
        }
    }
    [PunRPC]
    public void AddScore()
    {
        score++;
    }

    public void KillPlayer()
    {
        //gameObject.GetComponent<GameManager>().RespawnPlayer();
        GameObject.Find("GameManager").GetComponent<GameManager>().RespawnPlayer();
        PhotonNetwork.Destroy(gameObject);
    }
}
