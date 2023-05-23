using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //private GameObject attackTarget;
    private CharacterStates cStates;
    private P1 p1;
    private Animator anim;
    [Header("Attack")]
    List<string> animlist = new List<string>(new string[] { "Attack1", "Attack2", "Attack3" });
    public int combonum;//连招计数
    public float reset;
    private float resettime = 0.3f;
    //private float bullet_Count;
    //public float magezine_Clip;
    //private float shootTime;
    //private float reloadTime;
    public float speed = 500;

    // private bool isCritical;
    [Header("动画状态")]
    //private float lastAttackTime = 0;
    public bool isDead;
    private bool isJump;
    private bool isCombo;
    private bool isAttack2;
    private bool isAttack3;
    [Header("Skill图标")]
    public GameObject skillUI;
    [Header("Skill图标1")]
    public Image mask_img_1;//遮罩的图片
    private Text text_1;
    private bool canUseSkill_1; //是否可以使用技能
    public float skillCooldownTime_1; //技能冷却时间
    private float skillTimer_1 = 0;
    [Header("Skill图标2")]
    public Image mask_img_2;//遮罩的图片
    private Text text_2;
    private bool canUseSkill_2; //是否可以使用技能
    public float skillCooldownTime_2; //技能冷却时间
    private float skillTimer_2 = 0;
    [Header("Skill图标3")]
    public Image mask_img_3;//遮罩的图片
    private Text text_3;
    private bool canUseSkill_3; //是否可以使用技能
    public float skillCooldownTime_3; //技能冷却时间
    private float skillTimer_3 = 0;
    RaycastHit hit;
    AnimatorStateInfo animatorInfo;
    void Awake()
    {
        anim = GetComponent<Animator>();
        cStates = GetComponent<CharacterStates>();
        p1 = GetComponent<P1>();
        skillUI=GameObject.Find("Skill_UI");
    }

    private void OnEnable()
    {
        GameManager.Instance.RigisterPlayer(cStates);
    }
    void Start()
    {
        mask_img_1=skillUI.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        text_1=skillUI.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        //skill2
        mask_img_2=skillUI.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        text_2=skillUI.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        //skill3
        mask_img_3=skillUI.transform.GetChild(3).GetChild(0).GetComponent<Image>();
        text_3=skillUI.transform.GetChild(3).GetChild(1).GetComponent<Text>();
        canUseSkill_1=true;
        canUseSkill_2=true;
        canUseSkill_3=true;


        //MouseManager.Instance.OnMouseClicked += MoveToTarget;//添加订阅方式+=，
        //MouseManager.Instance.OnEnemyClicked += EventAttack;//添加订阅方式+=，
    }
    void Update()
    {
        animatorInfo = anim.GetCurrentAnimatorStateInfo(1);
        isDead = cStates.CurrentHealth == 0;
        if (isDead)
        {
            GameManager.Instance.NotifyObserver();
            //Destroy(gameObject,2f);
        }
        if (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.J))
        {
            if (animatorInfo.IsName("Base"))
            {
                anim.SetTrigger("Attack1");
                reset = 0;
            }
            if ((animatorInfo.normalizedTime >= 0.9f)&& (animatorInfo.IsName("1")))
            {
                isAttack2=true;
                isAttack3=false;
                reset = 0;
            }
            if ((animatorInfo.normalizedTime >= 0.9f)&& (animatorInfo.IsName("2")))
            {
               isAttack2=false;
               isAttack3=true;
                reset = 0;
            }
            if ((animatorInfo.normalizedTime >= 0.9f)&& (animatorInfo.IsName("3")))
            {
               isAttack2=false;
               isAttack3=false;
                reset = 0;
            }
        }
        if ((animatorInfo.normalizedTime > 0.9f))
        {
            reset += Time.deltaTime;
            if (reset > resettime)//当超过时间间隔就回到初始状态
            {
                anim.SetTrigger("Reset");
                isAttack2=false;
               isAttack3=false;
                reset=0;
            }
        }
        // if(Input.GetKeyDown(KeyCode.Alpha1)){
        //     anim.SetTrigger("Skill01");
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha2)){
        //     anim.SetTrigger("Skill02");
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha3)){
        //     anim.SetTrigger("Skill03");
        // }

        SwitchAnimation();
        if(skillUI.gameObject.activeSelf==true){
            SwitchSkill();
        }
        

        // if (lastAttackTime > -2)
        //     lastAttackTime -= Time.deltaTime;

    }
    private void SwitchAnimation()
    {
        // anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        anim.SetBool("IsDead", isDead);
        //anim.SetBool("Combo", isCombo);
        anim.SetBool("Attack2", isAttack2);
        anim.SetBool("Attack3", isAttack3);
        // anim.SetBool("Fire_Single", isFire_Single);
        // anim.SetBool("Fire_Continue", isFire_Continue);
    }
    private void SwitchSkill()
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
            text_1.text=Mathf.Max((skillCooldownTime_1-skillTimer_1),0.0f)+"s";
            if (skillTimer_1 >= skillCooldownTime_1)
            {
                canUseSkill_1 = true;
                skillTimer_1 = 0;
            }
        }
        //skill2
       if(Input.GetKeyDown(KeyCode.Alpha2)&& canUseSkill_2){
            anim.SetTrigger("Skill02");
            canUseSkill_2=false;
            skillTimer_2=0;
        }
        if (canUseSkill_2 == false)
        {
            skillTimer_2+= Time.deltaTime;
            mask_img_2.fillAmount = skillTimer_2/ skillCooldownTime_2;
            text_2.text=Mathf.Max((skillCooldownTime_2-skillTimer_2),0.0f)+"s";
            if (skillTimer_2 >= skillCooldownTime_2)
            {
                canUseSkill_2 = true;
                skillTimer_2= 0;
            }
        }
        //skill3
       if(Input.GetKeyDown(KeyCode.Alpha3)&& canUseSkill_3){
            anim.SetTrigger("Skill03");
            canUseSkill_3=false;
            skillTimer_3=0;
        }
        if (canUseSkill_3 == false)
        {
            skillTimer_3+= Time.deltaTime;
            mask_img_3.fillAmount = skillTimer_3/ skillCooldownTime_3;
            text_3.text=Mathf.Max((skillCooldownTime_3-skillTimer_3),0.0f)+"s";
            if (skillTimer_3 >= skillCooldownTime_3)
            {
                canUseSkill_3 = true;
                skillTimer_3= 0;
            }
        }
    }
    // private void OnTriggerStay(Collider other) {
    //     Debug.Log(other.name);
    // }

    // private void Fire()
    // {
    //     if (bullet_Count > 0)
    //     {
    //         reloadTime = 2f;
    //         if (Input.GetMouseButtonDown(0))
    //         {
    //             if (!EventSystem.current.IsPointerOverGameObject()) //Checks if the mouse is not over a UI part
    //             {
    //             }
    //         }
    //     }
    //     else
    //     {
    //         // isFire_Single = false;
    //         // isFire_Continue = false;
    //         reloadTime -= Time.deltaTime;
    //         if (reloadTime < 0)
    //         {
    //             bullet_Count = magezine_Clip;
    //         }
    //     }
    // }

    #region Other
    // private void MoveToTarget(Vector3 target)
    // {//必须传入委托的相同参数
    //     StopAllCoroutines();//终止所有协程
    //     if(isDead) return;
    //     // agent.stoppingDistance=stopDistance;
    //     // agent.isStopped = false;//人物可移动
    //     // agent.destination = target;
    // }
    // private void EventAttack(GameObject target)
    // {
    //     if(isDead) return;
    //     if (target != null)
    //     {
    //         attackTarget = target;
    //         StartCoroutine(MoveToAttackTarget());
    //     }
    // }
    // IEnumerator MoveToAttackTarget()
    // {
    //     agent.isStopped = false;//设置可移动状态
    //     agent.stoppingDistance=cStates.attackDate.attackRange;
    //     transform.LookAt(attackTarget.transform);//朝向敌人
    //     //FIXME  :  修改攻击范围参数
    //     while (Vector3.Distance(attackTarget.transform.position, transform.position) > cStates.attackDate.attackRange)//判断距离
    //     {
    //         agent.destination = attackTarget.transform.position;
    //         yield return null;//每一帧结束再执行，直到结束
    //     }
    //     agent.isStopped = true;//人物停止
    //     if (lastAttackTime < 0)
    //     {
    //         cStates.isCritical = UnityEngine.Random.value < cStates.attackDate.criticalChance;
    //         anim.SetTrigger("Attack");//攻击动画
    //         anim.SetBool("Critical", cStates.isCritical);
    //         //重置冷却时间
    //         lastAttackTime = cStates.attackDate.coolDown;
    //     }
    // }
    #endregion
    // void Hit()
    // {
    //     if (attackTarget.CompareTag("Attackable"))
    //     {
    //         if (attackTarget.GetComponent<Rock>())
    //         {
    //             attackTarget.GetComponent<Rigidbody>().velocity = Vector3.one;
    //             attackTarget.GetComponent<Rock>().rockStates = Rock.RockStates.HitEnemy;
    //             attackTarget.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
    //         }
    //     }
    //     if (attackTarget != null && attackTarget.CompareTag("Enemy"))
    //     {
    //         var targetStates = attackTarget.GetComponent<CharacterStates>();
    //         targetStates.TakeDamage(cStates, targetStates);
    //     }

    // }
}
