using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;
using Photon.Realtime;

public class LaserController : MonoBehaviourPun
{
    public float travelSpeed;
    public float lifeTime;
    public int damage;
    public float fadeInTime;

    float destroyTime;
    SpriteRenderer laserSpriterenderer;
    Player owner;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.x * travelSpeed, transform.up.y * travelSpeed);
        destroyTime = Time.time + lifeTime;
        laserSpriterenderer = gameObject.GetComponent<SpriteRenderer>();
        //Color transparent = new Color(255, 255, 255, 0);
        //laserSpriterenderer.color = transparent;
        //laserSpriterenderer.DOFade(255, fadeInTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime <= Time.time)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerVariables>().IncomingDamage(damage, owner);
        }
        Destroy(gameObject);
    }

    public void SetOwner(Player value)
    {
        owner = value;
    }
}
