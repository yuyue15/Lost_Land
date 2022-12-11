using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public string scenes;
    public AudioSource audioSource,audioSource2;

    void LateUpdate()
    {
        scenes=SceneManager.GetSceneByName("H2 A").name;

        if(scenes=="H2 A")
        {
            audioSource.mute = true;
            audioSource2.mute = false;
        }
        else
        {
            audioSource.mute = false;
            audioSource2.mute = true;
        }
    }
}
