using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController2D : MonoBehaviourPun
{
    #region Public Fields
    [Tooltip("Put transforms for the firing positions")]
    public GameObject projectile;
    public Transform[] firePositions;
    public float turnSpeed;
    public float maxSpeed;
    public float fireCooldown;
    public float health;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    #endregion
    #region Private Fields

    Rigidbody2D shipBody;
    Vector3 mousePos;
    Vector3 objectPos;
    float timeStamp;
    float currentHP;
    CameraFollow mainCam;
    #endregion
    void Start()
    {
        shipBody = gameObject.GetComponent<Rigidbody2D>();
        mainCam = Camera.main.GetComponent<CameraFollow>();
        mainCam.playerShip = gameObject;
        mainCam.FollowActive(true);
        currentHP = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;
    }

    void FixedUpdate()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
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

    void OnDestroy()
    {
        mainCam.FollowActive(false);
        mainCam.playerShip = null;
    }

    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            PlayerController2D.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        //DontDestroyOnLoad(this.gameObject);
    }
}
