using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerShip;

    bool isFollowing = false;
    void Start()
    {
        
    }

    public void FollowActive(bool x)
    {
        isFollowing = x;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(playerShip.transform.position.x, playerShip.transform.position.y, -10);
        }
    }
}
