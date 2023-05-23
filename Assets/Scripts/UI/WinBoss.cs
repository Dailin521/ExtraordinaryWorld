using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WinBoss : MonoBehaviour
{
    public GameObject panel;
   public void EXIT(){
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void BACK(){
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
