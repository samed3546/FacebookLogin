using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class GetPick : MonoBehaviour
{
    public Text txtStatus;
    public GameObject ButtonLogin, Buttonout;
    //public GameObject ProfilePicture;
    public Image ProfilePicture;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            txtStatus.text = "Failed to Initialize the Facebook SDK";
        }

        if (FB.IsLoggedIn)
        {
            FB.API("/me?fields=name", HttpMethod.GET, DispName);
            FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
            ButtonLogin.SetActive(false); Buttonout.SetActive(true);
        }
        else
        {
            txtStatus.text = "Please login to continue.";
            ButtonLogin.SetActive(true); Buttonout.SetActive(false);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0; //pause
        }
        else
        {
            Time.timeScale = 1; //resume
        }
    }

    public void LoginWithFB()
    {
        var perms = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    public void LogoutFromFB()
    {
        FB.LogOut(); ButtonLogin.SetActive(true); Buttonout.SetActive(false);
		ProfilePicture.GetComponent<Image> ().sprite = null;
        txtStatus.text = "Please login to continue.";
    }

    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            txtStatus.text = result.Error;
        }
        else {
            InitCallback();
        }
    }

    void DispName(IResult result)
    {
        if (result.Error != null)
        {
            txtStatus.text = result.Error;
        }
        else
        {
            txtStatus.text = "Welcome " + result.ResultDictionary["name"];
        }
    }

    private void GetPicture(IGraphResult result)
    {
        if (result.Error == null && result.Texture != null)
        {
            ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }
}
