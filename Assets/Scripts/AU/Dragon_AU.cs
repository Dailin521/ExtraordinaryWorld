using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_AU : MonoBehaviour
{
    private GameObject au;
    public AudioSource dragon1_Au;
    public AudioSource dragon2_Au;
    public AudioSource slimeDead_Au;
    private void Start() {
        au=GameObject.FindGameObjectWithTag("Audio");
        dragon1_Au=au.gameObject.transform.Find("Dragon1_AU").GetComponent<AudioSource>();
        dragon2_Au=au.gameObject.transform.Find("Dragon2_AU").GetComponent<AudioSource>();
        slimeDead_Au=au.gameObject.transform.Find("SlimeDead_AU").GetComponent<AudioSource>();
    }
    public void Play_Dragon1_AU(){
        dragon1_Au.Play();
    }
    public void Play_Dragon2_AU(){
        dragon2_Au.Play();
    }
    public void Play_SlimeDead_AU(){
        slimeDead_Au.Play();
    }
}
