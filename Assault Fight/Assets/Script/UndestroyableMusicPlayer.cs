using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndestroyableMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        GameObject[] musicGameObjects = GameObject.FindGameObjectsWithTag("MainMusicPlayer");
        for (int i=0; i<musicGameObjects.Length; i++) {
            if (i >= 1) {
                Destroy(musicGameObjects[i]);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
