using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    public Image Blackout;
    public GameObject BlackOut;

    // Start is called before the first frame update
    void Start()
    {
        BlackOut = Blackout.gameObject;
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());      
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            Blackout.color = new Color(0, 0, 0, fadeCount);
        }
        BlackOut.SetActive(false);
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Blackout.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
