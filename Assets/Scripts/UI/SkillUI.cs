using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image mask_img_1;//遮罩的图片
    private bool canUseSkill_1; //是否可以使用技能
    public float skillCooldownTime_1; //技能冷却时间
    private float skillTimer_1 = 0;
    private Animator anim;
    void Start()
    {
        anim = GameManager.Instance.playerStates.GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)&& canUseSkill_1){
            anim.SetTrigger("Skill01");
            canUseSkill_1=false;
            skillTimer_1=0;
        }
        if (canUseSkill_1 == false)
        {
            skillTimer_1+= Time.deltaTime;
            mask_img_1.fillAmount = skillTimer_1/ skillCooldownTime_1;
            if (skillTimer_1 >= skillCooldownTime_1)
            {
                canUseSkill_1 = true;
                skillTimer_1 = 0;
            }
        }
        // if(Input.GetKeyDown(KeyCode.Alpha2)&& canUseSkill){
        //     anim.SetTrigger("Skill02");
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha3)&& canUseSkill){
        //     anim.SetTrigger("Skill03");
        // }
    }
}
