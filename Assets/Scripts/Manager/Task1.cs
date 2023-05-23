using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Singleton<Task1>
{
    public GameObject obj1;

    public void Obj1off(){
        obj1.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale=1;
    }
    public void Obj1Start(){
        Invoke("Obj1On",3);
        
    }
    void Obj1On(){
    obj1.SetActive(true);
    Time.timeScale=0.1f;
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
    }
}
