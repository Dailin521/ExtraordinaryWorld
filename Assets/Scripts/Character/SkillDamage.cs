using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    protected CharacterStates currentStates;
    protected GameObject obj1;
    protected int baseDamage=1;
    protected void Awake() {
        obj1=this.transform.parent.gameObject;
        currentStates = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>();
    }
    protected void OnTriggerEnter(Collider other) {
        if(other.tag=="Enemy"){
            //Debug.Log(other.name);
            var targetStates = other.GetComponent<CharacterStates>();
            //currentStates.isCritical = Random.value < currentStates.attackDate.criticalChance;
            targetStates.TakeDamage(baseDamage,currentStates,targetStates);
        }
    }
   protected void HideGam(){
        this.gameObject.SetActive(false);
    }
   protected  void ShowGam(){
        this.gameObject.SetActive(true);
    }
}
