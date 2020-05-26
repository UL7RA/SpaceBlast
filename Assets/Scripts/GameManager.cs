using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    public Transform[] spawnPoints;

    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player " + newPlayer.NickName + " entered the level!"); //you don't see this if you're the one who's connecting
        if(newPlayer.IsMasterClient)
        {
            Debug.Log("Master Client connected. Loading level...");
            LoadArena();
        }
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player p in players)
        {
            Debug.Log(p.NickName);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + "left the room.");
        if(otherPlayer.IsMasterClient)
        {
            Debug.Log("Master client disconnected. Closing level...");
            LoadArena(); //this doesn't look right.. perhaps it just reloads the level without kicking off everyone?
        }
    }
    #endregion

    #region Private Methods
    void LoadArena()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        PhotonNetwork.LoadLevel("Small"); //TODO: LoadLevel hardcoded to "Small", may need change
    }

    #endregion

    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            Debug.Log("Master Client is now " + PhotonNetwork.NickName);
        }
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("We are Instantiating LocalPlayer");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            int spawnPointNum = Random.Range((int)0, (int)spawnPoints.Length);
            Vector3 spawnLocation = spawnPoints[spawnPointNum].transform.position;
            GameObject newShip = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnLocation, Quaternion.identity, 0) as GameObject;
            PlayerController2D controlScript = newShip.GetComponent<PlayerController2D>();
            CameraFollow playerCamera = Camera.main.GetComponent<CameraFollow>();
            controlScript.mainCam = playerCamera;
            playerCamera.playerShip = newShip;
            playerCamera.FollowActive(true);
        }
    }
}
