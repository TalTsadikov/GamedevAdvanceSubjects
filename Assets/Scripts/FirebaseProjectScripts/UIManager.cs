using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject userDataUI;
    public GameObject scoreboardUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    public void ClearScreen() 
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
    }

    public void LoginScreen() 
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() 
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void UserDataScreen() 
    {
        ClearScreen();
        userDataUI.SetActive(true);
    }

    public void ScoreboardScreen() 
    {
        ClearScreen();
        scoreboardUI.SetActive(true);
    }
}
