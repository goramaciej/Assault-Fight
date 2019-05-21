using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDelayed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start invoke method");
        Invoke("ShowLevel1", 5);
    }
    private void ShowLevel1() {
        //Debug.Log("It's time to show the next level");
        SceneManager.LoadScene(1);
    }
}
