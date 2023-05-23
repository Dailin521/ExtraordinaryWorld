using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Mouse_Pause : Singleton<Mouse_Pause>
{
    
    private CinemachineFreeLook followCamera;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Start() {
        followCamera = FindObjectOfType<CinemachineFreeLook>();
    }
    public void MouseShow(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;//鼠标显示并解锁
        if(followCamera!=null){
            followCamera.gameObject.SetActive(false);
        }
    }
    public void MouseHade(){
        Cursor.lockState = CursorLockMode.Locked;//鼠标隐藏并锁定
        if(followCamera!=null){
            followCamera.gameObject.SetActive(true);
        }
    }
}
