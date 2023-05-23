using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class R_Tri : MonoBehaviour
{
    public GameObject R;
    public GameObject talkUI;
    public bool isShow=false;

    private void OnTriggerStay(Collider other) {
        R.SetActive(true);
        if(Input.GetKey(KeyCode.R)){
            talkUI.SetActive(true);
            isShow=true;
        }

    }
    private void OnTriggerExit(Collider other) {
       R.SetActive(false);
            isShow=false;
    }
}
