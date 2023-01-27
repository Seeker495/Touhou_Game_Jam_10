using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Fade
{
    private static GameObject FADE_OBJECT;

    public static void SetUp(GameObject fadeObject = null)
    {
        if(fadeObject)
            FADE_OBJECT = Object.Instantiate(fadeObject, GameObject.FindWithTag("Canvas").transform);
        else if(FADE_OBJECT)
            FADE_OBJECT = Object.Instantiate(FADE_OBJECT, GameObject.FindWithTag("Canvas").transform);
    }
    public static void FadeIn(float duration = 1.0f)
    {
        FADE_OBJECT.GetComponent<Image>().DOFade(0.0f, duration).SetUpdate(true);
    }

    public static void FadeOut(float duration = 1.0f)
    {
        FADE_OBJECT.GetComponent<Image>().DOFade(1.0f, duration).SetUpdate(true);
    }

}
