using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFader : MonoBehaviour
{

    public CanvasGroup uiElement;
    //public List<CanvasGroup> uiElements;

    public void FadeIn(int level)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, .5f));

        if(level == 0)
        {
            GameObject.Find("Main Canvas").GetComponent<Canvas>().sortingOrder = 1;
            GameObject.Find("Blocker").GetComponent<Canvas>().sortingOrder = -1;
        }
        else if(level == 1){
            GameObject.Find("Main Canvas").GetComponent<Canvas>().sortingOrder = -1;
            GameObject.Find("Blocker").GetComponent<Canvas>().sortingOrder = -1;

        } 

    }

    public void FadeOut(int level)
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, .5f));

        if (level == 0)
        {
            GameObject.Find("Main Canvas").GetComponent<Canvas>().sortingOrder = -1;
            GameObject.Find("Blocker").GetComponent<Canvas>().sortingOrder = -1;
        }
        else if (level == 1)
        {
            GameObject.Find("Main Canvas").GetComponent<Canvas>().sortingOrder = 1;
            GameObject.Find("Blocker").GetComponent<Canvas>().sortingOrder = -1;

        } else
        {
            GameObject.Find("Main Canvas").GetComponent<Canvas>().sortingOrder = -1;
            GameObject.Find("Blocker").GetComponent<Canvas>().sortingOrder = 1;

        }

    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

        print("done");
    }
}

