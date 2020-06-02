using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerVariables : MonoBehaviourPun
{
    public int startingHealth;
    public int health;
    public int score;
    public Slider healthBar;
    public TextMeshPro nameBar;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        nameBar.text = photonView.Owner.NickName;
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
        GameObject.Find("GameManager").GetComponent<GameManager>().RespawnPlayer();
        PhotonNetwork.Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Instantiate(Resources.Load("ExplosionParticle"), transform.position, Quaternion.identity);
    }
}
