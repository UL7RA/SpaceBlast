using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController2D : MonoBehaviour
{
    #region Public Fields
    [Tooltip("Put transforms for the firing positions")]
    public GameObject projectile;
    public Transform[] firePositions;
    public float turnSpeed;
    public float maxSpeed;
    public float fireCooldown;
    public float health;
    #endregion
    #region Private Fields

    Rigidbody2D shipBody;
    Vector3 mousePos;
    Vector3 objectPos;
    float timeStamp;
    #endregion
    void Start()
    {
        shipBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;
    }

    void FixedUpdate()
    {
        MoveTowardsMouse();
        if (Input.GetAxis("Fire1") > 0 && timeStamp <= Time.time)
        {
            Fire();
            timeStamp = Time.time + fireCooldown;
        }
    }

    #region Private Methods
    void Fire()
    {
        foreach(Transform firePositionTransform in firePositions)
        {
            Vector3 fireCoords = firePositionTransform.position;
            GameObject laser = Instantiate(projectile, fireCoords, transform.rotation) as GameObject;
        }
    }

    void MoveTowardsMouse()
    {
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
        Quaternion toInterpolate = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, toInterpolate, Time.deltaTime * turnSpeed);
        shipBody.velocity = new Vector2(mousePos.normalized.x * maxSpeed, mousePos.normalized.y * maxSpeed);
    }
    #endregion
}
