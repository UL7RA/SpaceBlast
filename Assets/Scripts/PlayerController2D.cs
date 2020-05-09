using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController2D : MonoBehaviour
{
    #region Public Fields
    [Tooltip("Put transforms for the firing positions")]
    public Transform[] firePositions;
    public float turnSpeed;
    #endregion
    #region Private Fields
    Rigidbody2D shipBody;
    #endregion
    void Start()
    {
        shipBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        MoveTowardsMouse();
        if(Input.GetMouseButtonDown(0))
        {

        }
    }

    #region Private Methods
    void Fire()
    {

    }

    void MoveTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
        Quaternion toInterpolate = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, toInterpolate, Time.deltaTime * turnSpeed);

        //shipBody.AddForce(Mathf.);

    }
    #endregion
}
