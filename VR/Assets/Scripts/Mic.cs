using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic : MonoBehaviour
{

    AudioSource aud;

    void Start() {
        aud = GetComponent<AudioSource>();
    }


    public void PlaySnd() { 
        aud.Play();
        Debug.Log("PlaySound");
    }

    public void RecSnd() {
        aud.clip = Microphone.Start(Microphone.devices[0].ToString(), false, 3, 44100);
        Debug.Log("Record");
    }


}
