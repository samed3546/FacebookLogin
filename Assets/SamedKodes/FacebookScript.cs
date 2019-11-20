using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookScript : MonoBehaviour
{

    public Text FriendsText;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                FriendsText.text ="Couldn't initialize";
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
       
    }


    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion

    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://www.youtube.com"), "Hemen Dene",
            "video izle..",
            new System.Uri("https://i.hizliresim.com/J36Rjq.jpg"));
    }

    #region Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Yeni videolar burada", title: "Youtube Video");
    }

    public void FacebookInvite()
    {
       // FB.Mobile.AppInvite(new System.Uri("https://www.youtube.com"));
    }
    #endregion

    public void GetFriendsPlayingThisGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            FriendsText.text = string.Empty;
            foreach (var dict in friendsList)
                FriendsText.text += ((Dictionary<string, object>)dict)["name"];
        });
    }
}
// keytool -exportcert -alias test -keystore "C:\keys\FaceLogin.keystore" | "C:\OpenSSL-Win64\bin\openssl" sha1 -binary | "C:\OpenSSL-Win64\bin\openssl" base64
