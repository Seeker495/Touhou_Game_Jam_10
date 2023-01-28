using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Fade
{
    private static GameObject FADE_OBJECT;
    private static GameObject SAVE_FADE_OBJECT;
    public static void SetUp(GameObject fadeObject = null)
    {
        if (fadeObject != null)
            SAVE_FADE_OBJECT = fadeObject;
            FADE_OBJECT = Object.Instantiate(SAVE_FADE_OBJECT, GameObject.FindWithTag("Canvas").transform);
    }
    public static void FadeIn(float duration = 1.0f)
    {
        FADE_OBJECT.GetComponent<Image>().DOFade(0.0f, duration).SetUpdate(true);
    }

    public static void FadeOut(float duration = 1.0f)
    {
        FADE_OBJECT.GetComponent<Image>().DOFade(1.0f, duration).SetUpdate(true);
    }

    public static void FadeOut_with_Scene(MonoBehaviour monoBehavior,string sceneName, float duration = 1.0f)
    {
        FADE_OBJECT.GetComponent<Image>().DOFade(1.0f, duration).SetUpdate(true).OnComplete(() => SceneManager.LoadSceneAsync(sceneName));
    }

}
