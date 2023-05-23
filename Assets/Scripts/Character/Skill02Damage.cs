using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill02Damage : SkillDamage
{
    void Start()
    {
        baseDamage=50*currentStates.attackDate.currentLevel;
        Invoke("HideGam",0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject){
            this.transform.Translate(Vector3.forward*20* Time.deltaTime,Space.Self);
        }
    }
}
