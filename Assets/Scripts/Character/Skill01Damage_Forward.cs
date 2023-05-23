using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill01Damage_Forward : SkillDamage
{
    // Start is called before the first frame update
    void Start()
    {
        baseDamage=20*currentStates.attackDate.currentLevel;
        HideGam();
        Invoke("ShowGam",2.2f);
        Invoke("HideGam",2.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject){
            this.transform.Translate(Vector3.forward*20* Time.deltaTime,Space.Self);
        }
    }
}
