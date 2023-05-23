using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    [Header("Transition Info")]
    public string sceneName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;
    public bool canTrans;
    public bool Trans;
    private float timeInterval = 8f;
    private float tempTime;
    private bool beginCD = false;
    private void Update()
    {
        if (beginCD)
        {
            tempTime += Time.deltaTime;
            canTrans=false;
            if (tempTime >= timeInterval)
            {
                tempTime = 0;
                beginCD=false;
                Trans=false;
                canTrans=true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && canTrans && beginCD == false)
        {
            Trans=true;

            //TODO: 传送
            //SceneController.Instance.TransitionToDestination(this);

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = true;
            if(Trans&&!beginCD&&beginCD == false){
                beginCD = true;
                Trans=false;
                    //SceneController.Instance.TransitionToDestination(this);
                Invoke("StartTran",0.1f);
                canTrans=false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = false;
        }
    }
    void StartTran(){
        SceneController.Instance.TransitionToDestination(this);
        canTrans=false;
    }
}
