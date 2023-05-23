using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Settings_Main;
    private GameObject PlayerChoice_Canvas;
    //private GameObject MainUI_Canvas;

    public void MainStart_Button_Enter(){
        PlayerChoice_Canvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void MainSettings_Button_Enter(){
        Settings_Main.SetActive(true);
    }
    public void ExitGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
