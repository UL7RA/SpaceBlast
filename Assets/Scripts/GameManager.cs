using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player " + newPlayer.NickName + " entered the level!"); //you don't see this if you're the one who's connecting
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Client connected. Loading level...");
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + "left the room.");
        if(PhotonNetwork.IsMasterClient)
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
}
