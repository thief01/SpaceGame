using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField mail;
    [SerializeField] private TMP_InputField displayName;
    
    
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest()
        {
            Email = mail.text,
            Username = displayName.text,
            Password = password.text,
            DisplayName = displayName.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailed);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email = mail.text,
            Password = password.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams(){GetUserAccountInfo = true, GetTitleData = true}
        };
        
        
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailed);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        //Debug.LogError(result.ErrorMessage);
    }

    private void OnRegisterFailed(PlayFabError result)
    {
        Debug.LogError(result.ErrorMessage);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        // load scene
        PhotonNetwork.NickName = result.InfoResultPayload.AccountInfo.TitleInfo.DisplayName;
        SceneManager.LoadScene(1);
    }

    private void OnLoginFailed(PlayFabError result)
    {
        Debug.LogError(result.ErrorMessage);
    }
}