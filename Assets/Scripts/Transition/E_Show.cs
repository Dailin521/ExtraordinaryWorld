using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Show : MonoBehaviour
{
     public GameObject R;

    private void OnTriggerStay(Collider other) {
        R.SetActive(true);
    }
    private void OnTriggerExit(Collider other) {
       R.SetActive(false);
    }
}
