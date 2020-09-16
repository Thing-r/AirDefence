using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void Game()
    {

        SceneLoadAsync.Instance.LoadGameScene();
    }

    public void Settings()
    {
        SceneLoadSync.Instance.LoadContolInfo();
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void Intro()
    {
        SceneLoadSync.Instance.LoadContolIntro();
    }

    public void TryToFly()
    {
        SceneLoadSync.Instance.LoadContolTryToFly();
    }
}
