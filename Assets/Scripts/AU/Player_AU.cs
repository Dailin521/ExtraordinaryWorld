using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AU : MonoBehaviour
{
    private GameObject au;
    public AudioSource attack1_Au;
    public AudioSource attack2_Au;
    public AudioSource attack3_Au;
    private void Start() {
        au=GameObject.FindGameObjectWithTag("Audio");
        attack1_Au=au.transform.GetChild(1).GetComponent<AudioSource>();
        attack2_Au=au.transform.GetChild(2).GetComponent<AudioSource>();
        attack3_Au=au.transform.GetChild(3).GetComponent<AudioSource>();
    }
    public void Play_Attack1_AU(){
        attack1_Au.Play();
    }
    public void Play_Attack2_AU(){
        attack2_Au.Play();
    }
    public void Play_Attack3_AU(){
        attack3_Au.Play();
    }
}
