using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SoundPlayer
{
    private static SoundManager SoundManager;
    public static float BGM_Volume;
    public static float SFX_Volume;
    public static void SetUp(GameObject soundObj)
    {
        soundObj.TryGetComponent(out SoundManager);
    }

    public static void PlayBGM(in eBGM type)
    {
        SoundManager.PlayBGM(type);
    }

    public static void PlaySFX(in eSFX type, in bool isLoop = false)
    {
        SoundManager.PlaySFX(type, isLoop);
    }

    public static void PlaySFX_With_Scene(in eSFX type, in string scene)
    {
        SoundManager.StartCoroutine(GoToScene(type, scene));
    }

    public static void StopBGM()
    {
        SoundManager.StopBGM();
    }

    public static void StopSFX()
    {
        SoundManager.StopSFX();
    }
    private static IEnumerator GoToScene(eSFX type, string scene)
    {
        AudioSource audioPlayer = SoundManager.GetUnUsedSFXPlayer();
        SoundManager.PlaySFX(type);
        var nextScene = SceneManager.LoadSceneAsync(scene);
        nextScene.allowSceneActivation = false;
        while (!nextScene.allowSceneActivation)
        {
            yield return new WaitForFixedUpdate();
            if(!audioPlayer.isPlaying)
            {
                nextScene.allowSceneActivation = true;
            }
        }
    }
}
