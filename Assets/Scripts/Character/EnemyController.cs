using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStates))]//判断并添加相关组件
public class EnemyController : MonoBehaviour, IEendGameObserver
{
    private EnemyStates enemyStates;//敌人转台
    private NavMeshAgent agent;//代理组件
    protected GameObject attackTarget;//攻击目标
    private Animator anim;//动画控制
    private Collider coll;//碰撞器
    protected CharacterStates cStates;//player
    private float speed;//记录初始速度
    //配合动画
    private bool isWalk;
    private bool isFollow;
    protected bool isDead;
    private bool playerDead;
    //private bool isCritical;
    [Header("Basic Settings")]
    public float sightRadius;//可视范围
    public bool isGuard;//这个不是动画
    public float lookAtTime;
    private float remainLookAtTime;
    private float lastAttackTime;
    private Quaternion guardRotation;
    [Header("Patrol States")]
    public float patrolRange;//巡逻范围
    private Vector3 wayPoint;
    private Vector3 guardPos;//记录初始位置
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;
        guardPos = transform.position;
        guardRotation = transform.rotation;
        remainLookAtTime = lookAtTime;
        cStates = GetComponent<CharacterStates>();
        coll = GetComponent<Collider>();
    }
    void Start()
    {
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PATROL;
            GetNewWayPoint();
        }
        //FIXME:场景切换后修改掉
        GameManager.Instance.AddObserver(this);
    }
    //场景切换时启用
    private void OnEnable()
    {
        //GameManager.Instance.AddObserver(this);
    }
    void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }
    void Update()
    {
        if (cStates.CurrentHealth == 0)
        {
            isDead = true;
        }
        if (!playerDead)
        {
            SwitchStates();
            SwitchAnimation();
            if (lastAttackTime > -10)
            {
                lastAttackTime -= Time.deltaTime;
            }
        }

    }
    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", cStates.isCritical);
        anim.SetBool("Death", isDead);
    }
    void SwitchStates()
    {   //判断是否找到敌人
        if (isDead)
        {
            enemyStates = EnemyStates.DEAD;
        }
        else if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
        }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                isFollow = false;
                if (transform.position != guardPos)
                {
                    isWalk = true;
                    agent.isStopped = false;
                    agent.destination = guardPos;
                    //性能节省一点点
                    if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
                    }
                }
                break;
            case EnemyStates.PATROL:
                isFollow = false;
                agent.speed = speed * 0.5f;
                //判断是否到了随机点
                if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance+0.1f)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else
                    {
                        //GetNewWayPoint();
                        Invoke("GetNewWayPoint",0.5f);
                    }
                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyStates.CHASE:
                //TODO:追player

                //TODO:配合动画
                isWalk = false;
                agent.speed = speed;
                //TODO:在攻击范围内攻击
                if (!FoundPlayer())//没有发现敌人
                {
                    //TODO:拉脱回到上一个状态
                    isFollow = false;

                    if (remainLookAtTime > 0)
                    {
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if (isGuard)
                    {
                        enemyStates = EnemyStates.GUARD;
                    }
                    else
                    {
                        enemyStates = EnemyStates.PATROL;
                        remainLookAtTime = lookAtTime;
                    }

                }
                else
                {
                    //发现敌人

                    if (TargetInAttackRange() || TargetInSkillRange())
                    {//
                        agent.isStopped = true;
                        isFollow = false;
                        isWalk = false;
                        if (lastAttackTime < 0)
                        {
                            lastAttackTime = cStates.attackDate.coolDown;
                            //暴击判断
                            cStates.isCritical = Random.value < cStates.attackDate.criticalChance;
                            //执行攻击
                            Attack();
                        }
                    }
                    else
                    {
                        //isFollow = true;
                        agent.isStopped = false;
                        agent.destination = attackTarget.transform.position;
                    }
                }

                break;
            case EnemyStates.DEAD:
                
                Invoke("EnemyDead",1);
                break;
        }
    }
    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        //transform.rotation = Quaternion.Lerp(transform.rotation, attackTarget.transform.rotation, 0.01f);
        if (transform.IsFacingTarget(attackTarget.transform))
        {
            if (TargetInSkillRange() && !TargetInAttackRange())
            {
                //远程动画
                anim.SetTrigger("Skill");
            }
            if (TargetInAttackRange())
            {
                //近身动画
                anim.SetTrigger("Attack");
            }
        }

    }
    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);//var 任何类型
        foreach (var target in colliders)//遍历
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }
    //
    bool TargetInAttackRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= cStates.attackDate.attackRange;
        }
        else
        {
            return false;
        }

    }
    bool TargetInSkillRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= cStates.attackDate.skillRange;
        }
        else
        {
            return false;
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
        Gizmos.color = Color.red;//patrolRange是范围大小
        Gizmos.DrawWireSphere(transform.position, sightRadius);//patrolRange是范围大小
    }
    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;//复原等待看的时间
        float randomX = Random.Range(-patrolRange, patrolRange);//获得新的x偏移量
        float randomZ = Random.Range(-patrolRange, patrolRange);
        //guards 是初始中心点坐标
        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
        //FIXME: 可能出现的问题
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 3) ? hit.position : transform.position;
        
    }
    void Hit()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStates = attackTarget.GetComponent<CharacterStates>();
            targetStates.TakeDamage(cStates, targetStates);
        }
        else
        {
            Debug.Log("闪避");
        }
    }

    public void EndNotify()
    {
        //获胜动画
        //停止所有移动
        //停止Agent
        anim.SetBool("Win", true);
        playerDead = true;
        isFollow = false;
        isWalk = false;
        attackTarget = null;
    }
    void EnemyDead(){
        coll.enabled = false;
                agent.radius = 0;
                Destroy(gameObject, 1f);
    }
}
