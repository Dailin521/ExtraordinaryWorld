using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_AU : MonoBehaviour
{
    private GameObject au;
    public AudioSource slime1_Au;
    public AudioSource slime2_Au;
    public AudioSource slimeDead_Au;
    private void Start() {
        au=GameObject.FindGameObjectWithTag("Audio");
        slime1_Au=au.gameObject.transform.Find("Slime1_AU").GetComponent<AudioSource>();
        slime2_Au=au.gameObject.transform.Find("Slime2_AU").GetComponent<AudioSource>();
        slimeDead_Au=au.gameObject.transform.Find("SlimeDead_AU").GetComponent<AudioSource>();
    }
    public void Play_Slime1_AU(){
        slime1_Au.Play();
    }
    public void Play_Slime2_AU(){
        slime2_Au.Play();
    }
    public void Play_SlimeDead_AU(){
        slimeDead_Au.Play();
    }

}
