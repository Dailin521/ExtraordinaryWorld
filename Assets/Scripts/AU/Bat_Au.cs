using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Au : MonoBehaviour
{
    private GameObject au;
    public AudioSource bat1_Au;
    public AudioSource bat2_Au;
    public AudioSource slimeDead_Au;
    private void Start() {
        au=GameObject.FindGameObjectWithTag("Audio");
        bat1_Au=au.gameObject.transform.Find("Bat1_AU").GetComponent<AudioSource>();
        bat2_Au=au.gameObject.transform.Find("Bat2_AU").GetComponent<AudioSource>();
        slimeDead_Au=au.gameObject.transform.Find("SlimeDead_AU").GetComponent<AudioSource>();
    }
    public void Play_Bat1_AU(){
        bat1_Au.Play();
    }
    public void Play_Bat2_AU(){
        bat2_Au.Play();
    }
    public void Play_SlimeDead_AU(){
        slimeDead_Au.Play();
    }
}
