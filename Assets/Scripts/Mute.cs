using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour
{
    AudioSource som;

    private void Start()
    {
        som = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("PPSom") == 1)
        {
            som.mute = false;
        }
        else
        {
            som.mute = true;
        }
    }
}
