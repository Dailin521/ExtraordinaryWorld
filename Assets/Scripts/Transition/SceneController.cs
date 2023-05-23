using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    GameObject player;
    public GameObject playerPrefab;
    //public GameObject cu be;
    public GameObject[] tar;
    CharacterController cc;
    // cc;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }
    }
    public void TransitionToDestination(TransitionPoint_2 transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint_2.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint_2.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }
    }
    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SaveManager.Instance.SavePlayerDate();
            // player = GameManager.Instance.playerStates.gameObject;
            // Destroy(player);
            GameManager.isStartGame=1;
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            SaveManager.Instance.LoadPlayerDate();
            yield break;
        }
        else
        {
            player = GameManager.Instance.playerStates.gameObject;
            cc = player.GetComponent<CharacterController>();
            //cc.enabled=false;
            player.SetActive(false);
            Debug.Log("传送启动");
            //player.transform.SetPositionAndRotation(GetDestination1(destinationTag).transform.position,GetDestination1(destinationTag).transform.rotation);
            player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            //cc.enabled=true;
            Invoke("PlayerOn", 1f);//1秒后玩家显示
            yield return null;
        }
    }
    private GameObject GetDestination1(TransitionDestination.DestinationTag destinationTag)
    {
        for (int i = 0; i < tar.Length; i++)
        {
            if (tar[i].GetComponent<TransitionDestination>().destinationTag == destinationTag)
                return tar[i];
        }

        return null;
    }
    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        return null;
    }
    void PlayerOn()
    {
        player.SetActive(true);
    }
}
