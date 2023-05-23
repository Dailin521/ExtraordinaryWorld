using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Main : MonoBehaviour
{
    public GameObject settings_Main_Canvas;
    public GameObject main_Canvas;
    public GameObject settings_Sound_Canvas;
    public GameObject Sounds;
    public GameObject SoundsOff;
    public Slider slider;
    public Text text;
    Start_Game start_Game;
    public void SMain_Back()
    {
        if (!main_Canvas.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            settings_Main_Canvas.SetActive(false);
            Time.timeScale = 1;
            Mouse_Pause.Instance.MouseHade();
        }
    }
    public void SSound_Back()
    {
        if(main_Canvas.activeSelf){
            settings_Sound_Canvas.SetActive(false);
        }else{
        settings_Main_Canvas.SetActive(true);
        settings_Sound_Canvas.SetActive(false);
        }
    }
    public void SMainUI_Enter()
    {
        main_Canvas.SetActive(true);
        settings_Main_Canvas.SetActive(false);
        if (Time.timeScale == 0)
        {
            text.text = "继续游戏";
        }
    }
    public void SSound_Enter()
    {
        settings_Sound_Canvas.SetActive(true);
        settings_Main_Canvas.SetActive(false);
    }



    public void MainS_Canvas_Sounds_Off()
    {
        SoundsOff.SetActive(true);
        Sounds.SetActive(false);
        slider.value = 0;

    }
    public void MainS_Canvas_Sounds_On()
    {
        Sounds.SetActive(true);
        SoundsOff.SetActive(false);
    }
}
