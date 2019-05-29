using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDelayed : MonoBehaviour {
    [SerializeField] GameObject startButton;
    //[SerializeField] Slider slider;

    private bool loadScene = false;

    void Start() {
        //slider.gameObject.SetActive(false);
        //StartCoroutine(LoadNewScene("Level1"));

        Invoke("SetButton", 4f);
    }
    void SetButton() {
        startButton.SetActive(true);
    }

    /*IEnumerator LoadNewScene(string sceneName) {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone) {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }*/


    public void ShowLevel1() {
        //Debug.Log("It's time to show the next level");
        SceneManager.LoadScene(1);
    }
}
