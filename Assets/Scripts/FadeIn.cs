using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{

    public GameObject panelFadeIn;
    public Animator animFade;
    public float timeLoad;
    // Start is called before the first frame update
    void Start()
    {
        panelFadeIn = GameObject.FindWithTag("FadeIn");
        animFade = panelFadeIn.GetComponent<Animator>();
        panelFadeIn.SetActive(false);

    }

    
    public void FadeInScene()
    {
        panelFadeIn.SetActive(true);
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {

        yield return new WaitForSeconds(timeLoad);
        GameController.instance.LoadScenes("GamePlay");

    }
}
