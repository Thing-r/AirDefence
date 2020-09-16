using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadSync : SingletonMngr<SceneLoadSync>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected SceneLoadSync() { }

    public void LoadContolIntro()
    {
        StartCoroutine(LoadDelayTimer("r1Intro", 2f));
    }

    public void LoadContolInfo()
    {
        // SceneManager.LoadScene("Load");
        StartCoroutine(LoadDelayTimer("r2CtrlSettings", 1f));
    }

    public void LoadContolTryToFly()
    {
        StartCoroutine(LoadDelayTimer("r3TryToFly", 2f));
    }

    private IEnumerator LoadDelayTimer(string sceneName, float secs)  // scene and time
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(sceneName);
    }


}
