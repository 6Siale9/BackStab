using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void BtnFight()
    {
        SceneManager.LoadScene(1);
    }
    public void BtnLeave()
    {
        Application.Quit();
    }
    public void BtnExplanation()
    {
        SceneManager.LoadScene(2);
    }
    public void BtnCredit()
    {
        SceneManager.LoadScene(3);
    }

    public void BtnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
