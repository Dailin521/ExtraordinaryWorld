using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill03Damage : MonoBehaviour
{
    // Start is called before the first frame update
    protected CharacterStates currentStates;
    private void Awake() {
        currentStates = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>();
    }

    // Update is called once per frame
    private void Start() {
        Invoke("HideGam",0.4f);
    }
    void Update()
    {
        FoundEnemy();
    }
    void FoundEnemy()
    {
        var colliders = Physics.OverlapSphere(transform.position, 12);//var 任何类型
        foreach (var target in colliders)//遍历
        {
            if (target.CompareTag("Enemy"))
            {
                var targetStates = target.GetComponent<CharacterStates>();
                targetStates.TakeDamage((0.1f*currentStates.attackDate.currentLevel*Time.deltaTime),currentStates,targetStates);
            }
        }
    }
     void HideGam(){
        this.gameObject.SetActive(false);
    }
}
