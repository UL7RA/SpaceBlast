using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Realtime;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class NicknameInputField : MonoBehaviour
{
    #region Private Constants

    const string playerNamePrefKey = "PlayerName";

    #endregion

    #region MonoBehaviour Callbacks

    void Start()
    {
        string nickname = "USS Spaceship";
        InputField field = this.GetComponent<InputField>();
        if(field != null)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                nickname = PlayerPrefs.GetString(playerNamePrefKey);
                field.text = nickname;
            }
        }
        PhotonNetwork.NickName = name;
    }

    #endregion

    #region Public Methods
    //TODO: probably will need to change to OnEndEdit
    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player name is null or empty!");
            return;
        }
        Debug.Log("Player nick changed to " + value);
        PlayerPrefs.SetString(playerNamePrefKey, value);
        PhotonNetwork.NickName = value;
    }
    #endregion
}
