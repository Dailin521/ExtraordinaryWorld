using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AU : MonoBehaviour
{
    AudioSource au;
    private void Awake() {
        au=this.gameObject.GetComponent<AudioSource>();
    }
    public void Play_AU(){
        au.Play();
    }
    public void Stop_AU(){
        au.Stop();
    }

}
