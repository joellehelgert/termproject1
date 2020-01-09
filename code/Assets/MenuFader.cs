using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFader : MonoBehaviour
{
    // Start is called before the first frame update

    public CanvasGroup uiElement; 

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        float _timeStartedLurping = Time.time;
        float timeSinceStarted = Time.time + _timeStartedLurping;
        float persentageComple = timeSinceStarted / lerpTime; 

        while (true)
        {
            timeSinceStarted = Time.time + _timeStartedLurping; 

            persentageComple = timeSinceStarted / lerpTime;


            yield return new WaitForEndOfFrame(); 

        }
    }
}
