using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Net.Sockets;

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    public MonoBehaviour[] localScripts;

    public float lerpSpeed;

    PlayerVariables playerVars;
    Vector3 latestPosition;
    Vector3 velocity;
    Quaternion latestRotation;
    Rigidbody2D shipBody;

    // Start is called before the first frame update

    void Awake()
    {
        shipBody = gameObject.GetComponent<Rigidbody2D>();
        playerVars = gameObject.GetComponent<PlayerVariables>();
        if (photonView.IsMine)
        {
            //Player is local
        }
        else
        {
            //Player is Remote, deactivate the scripts and object that should only be enabled for the local player
            for (int i = 0; i < localScripts.Length; i++)
            {
                localScripts[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, latestPosition, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRotation, Time.deltaTime * lerpSpeed);
            //shipBody.velocity = velocity;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(playerVars.health);
            stream.SendNext(playerVars.score);
        }
        else
        {
            // Network player, receive data
            latestPosition = (Vector3)stream.ReceiveNext();
            latestRotation = (Quaternion)stream.ReceiveNext();
            playerVars.health = (int)stream.ReceiveNext();
            playerVars.score = (int)stream.ReceiveNext();
        }
    }
}
