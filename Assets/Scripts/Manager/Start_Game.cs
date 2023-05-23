using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Game : MonoBehaviour
{
    //public static int isStartGame;
    public GameObject skillUI;
    public GameObject player;
    public GameObject camera01;
    public GameObject playerHealthBar;
    public GameObject mainCanvas;
    //public GameObject CM_FreeLook1;
    public Transform bornPosition;
    public CinemachineFreeLook freeLook;
    //public static int isStartGame;
    public GameObject playera = null;
    public GameObject Playera
    {
        get
        {
            if (playera != null)
            {
                return playera;
            }
            else return null;
        }
        set
        {
            playera = value;
        }
    }
    protected void Awake()
    {
        DontDestroyOnLoad(this);
        //sStartGame=0;
    }
    public void MainCanvas_Start()
    {
        if (Playera != null)
        {
            Time.timeScale = 1;
            mainCanvas.SetActive(false);
            Debug.Log("11");
        }
        else
        {
            //isStartGame=1;
            skillUI.SetActive(true);
            playera = Instantiate(player, bornPosition.position, bornPosition.rotation);
            // CM_FreeLook1.transform.parent = playera.transform;
            freeLook.GetRig(0).LookAt =
                playera.transform.Find("LookPoint");
            mainCanvas.SetActive(false);
            camera01.SetActive(false);
            playerHealthBar.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Task1.Instance.Obj1Start();
        }

    }
}
