using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Controller : Singleton<Audio_Controller>
{
    public GameObject soundButton;
    public Slider slider;
    public AudioSource BGsound;
    public GameObject M;
    public GameObject M_img;
    public GameObject bagUI;
    public GameObject bag_img;
    public GameObject shopUI;
    public GameObject shop_img;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void ControlAudio(){
        if(soundButton){
            Volume();
        }
    }
    public void Volume(){
        BGsound.volume=slider.value;
    }
    public void ControlAudioOff(){
        slider.value=0;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.M)){
            if(M.activeSelf){
                M.SetActive(false);//关闭界面
                Mouse_Pause.Instance.MouseHade();
            }else{
                M.SetActive(true);
                Mouse_Pause.Instance.MouseShow();
            }
        }
    }
    public void Shop_UI(){
        shopUI.SetActive(true);
        bagUI.SetActive(false);
        M.SetActive(true);

        shop_img.SetActive(true);
        M_img.SetActive(false);
        bag_img.SetActive(false);
    }
    public void BAG_UI(){
        shopUI.SetActive(false);
        bagUI.SetActive(true);
        M.SetActive(true);

        shop_img.SetActive(false);
        M_img.SetActive(false);
        bag_img.SetActive(true);
    }
    public void Mission_UI(){
        shopUI.SetActive(false);
        bagUI.SetActive(false);
        M.SetActive(true);

        shop_img.SetActive(false);
        M_img.SetActive(true);
        bag_img.SetActive(false);
    }
    public void Buy_Button(){
        if(GameManager.Instance.playerStates.GoldPoint>=1){
           GameManager.Instance.playerStates.GoldPoint--;
           GameManager.Instance.playerStates.red_Medicine++;
        }
    }
    public void Use_Button(){
        if(GameManager.Instance.playerStates.red_Medicine>=1){
            GameManager.Instance.playerStates.red_Medicine--;
            GameManager.Instance.playerStates.CurrentHealth=Mathf.Max((GameManager.Instance.playerStates.CurrentHealth+50),GameManager.Instance.playerStates.MaxHealth);
        }
    }
    public void Use_Button_Full(){
        if(GameManager.Instance.playerStates.red_Medicine>=5){
            GameManager.Instance.playerStates.red_Medicine-=5;
            GameManager.Instance.playerStates.CurrentHealth=GameManager.Instance.playerStates.MaxHealth;
        }
    }
}
