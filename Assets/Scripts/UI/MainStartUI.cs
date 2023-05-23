using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStartUI : Singleton<MainStartUI>
{
    // Start is called before the first frame update
    public GameObject mainCanvas;
    public GameObject camera01;
    public GameObject playerHealthBar;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void OnEnable() {
        if (GameManager.isStartGame == 0)
        {
            mainCanvas.SetActive(true);
        }
        if(GameManager.isStartGame == 1)
        {
            mainCanvas.SetActive(false);
            camera01.SetActive(false);
            playerHealthBar.SetActive(true);
            Debug.Log("1");
        }
    }


    // Update is called once per frame

}
