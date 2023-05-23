using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    public static int isStartGame;
    public GameObject mainPanel;
    public GameObject cam1;
    public GameObject loseGame;
    public CharacterStates playerStates;
    private CinemachineFreeLook followCamera;
    public Transform bornPoint1;
    public Transform bornPoint2;
    public AudioSource bgAU;
    List<IEendGameObserver> endGameObservers = new List<IEendGameObserver>();
    private bool isReStart;
    public bool isJS_Mission;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        isReStart = false;
    }
    public void RigisterPlayer(CharacterStates player)
    {
        playerStates = player;
        isReStart = true;
        followCamera = FindObjectOfType<CinemachineFreeLook>();
        if (followCamera != null)
        {
            followCamera.Follow = playerStates.transform.GetChild(9);
            followCamera.LookAt = playerStates.transform.GetChild(9);
        }
    }
    public void AddObserver(IEendGameObserver observer)
    {
        endGameObservers.Add(observer);
    }
    public void RemoveObserver(IEendGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }
    public void NotifyObserver()
    {
        if (isReStart)
        {
            isReStart = false;
            playerStates.gameObject.SetActive(false);
            loseGame.SetActive(true);
            bgAU.Stop();
            loseGame.GetComponent<AudioSource>().Play();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        //Invoke("ReStart",1);
        // foreach (var observer in endGameObservers)
        // {
        //     observer.EndNotify();
        // }
    }
    public void ReStart()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Start_Night":
                playerStates.gameObject.transform.position = bornPoint1.position;
                Invoke("PlayerOn", 1);
                break;
            case "Challenge_Night":
                playerStates.gameObject.transform.position = bornPoint2.position;
                Invoke("PlayerOn", 1);
                break;
        }

        //playerStates.characterDate.currentHealth = playerStates.characterDate.maxHealth;
        //mainPanel.SetActive(true);
        //cam1.SetActive(true);
    }
    void PlayerOn()
    {
        loseGame.SetActive(false);
        playerStates.gameObject.SetActive(true);
        playerStates.characterDate.currentHealth = playerStates.characterDate.maxHealth;

        playerStates.gameObject.GetComponent<PlayerController>().isDead = false;
        Cursor.lockState = CursorLockMode.Locked;
        bgAU.Play();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
