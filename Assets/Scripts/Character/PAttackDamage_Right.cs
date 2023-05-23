using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackDamage_Right : MonoBehaviour
{
    CharacterStates currentStates;
    //NavMeshAgent agent;
    private GameObject obj1;
    private void Awake() {
        obj1=this.transform.parent.gameObject;
        currentStates = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>();
    }
    private void Start() {
        Invoke("Hidegam",0.25f);
    }
    private void Update() {
        if(this.gameObject){
            this.transform.RotateAround(obj1.transform.position,obj1.transform.up, 1000 * Time.deltaTime);
        }
    }
    void Hidegam(){
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Enemy"){
            //Debug.Log(other.name);
            var targetStates = other.GetComponent<CharacterStates>();
            currentStates.isCritical = Random.value < currentStates.attackDate.criticalChance;
            targetStates.TakeDamage(currentStates, targetStates);
        }
    }
}
