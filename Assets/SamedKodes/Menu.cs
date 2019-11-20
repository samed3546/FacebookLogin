using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Buttclick(int id)
    {
        if (id == 1)
        {
            SceneManager.LoadScene(1);
        }
        else {
            Application.Quit();
        }
    }
}
