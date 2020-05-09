using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using DG.Tweening;

public class LaserRedFastController : MonoBehaviour
{
    public float travelSpeed;
    public float lifeTime;
    public float damage;
    public float fadeInTime;

    private float destroyTime;
    private SpriteRenderer laserSpriterenderer;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.x * travelSpeed, transform.up.y * travelSpeed);
        destroyTime = Time.time + lifeTime;
        laserSpriterenderer = gameObject.GetComponent<SpriteRenderer>();
        Color transparent = new Color(255, 255, 255, 0);
        laserSpriterenderer.color = transparent;
        laserSpriterenderer.DOFade(255, fadeInTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime <= Time.time)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }
}
