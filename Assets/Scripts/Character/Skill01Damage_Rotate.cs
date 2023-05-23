using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill01Damage_Rotate : SkillDamage
{
    private void Start() {
        baseDamage=2*currentStates.attackDate.currentLevel;
        Invoke("HideGam",3);
    }
    void Update()
    {
        if(this.gameObject){
            this.transform.RotateAround(obj1.transform.position,obj1.transform.up, 2000* Time.deltaTime);
        }
    }
    void Hidegam(){
        this.gameObject.SetActive(false);
    }
}
