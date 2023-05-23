using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("Skill")]
    public float kickForce=25;
    public GameObject rockPrefab;
    public Transform headPos;
    public GameObject portal;
    public GameObject panel;
    private void FixedUpdate() {
        if(isDead){
            portal.SetActive(true);
            panel.SetActive(true);
            Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
        }
    }
    //Animation Event
    public void KickOff(){
        if(attackTarget!=null&&transform.IsFacingTarget(attackTarget.transform)){
            var targetStates=attackTarget.GetComponent<CharacterStates>();
            Vector3 direction=(attackTarget.transform.position-transform.position).normalized;
           // direction.Normalize();
           targetStates.GetComponent<CharacterController>().Move(direction*kickForce);
           //击晕效果
           //targetStates.GetComponent<Animator>().SetTrigger("Dizzy");

            targetStates.TakeDamage(cStates,targetStates);
        }
    }
    //Animation Event
    public void ThrowRock(){
        if(attackTarget!=null){
            var rock=Instantiate(rockPrefab,headPos.position,Quaternion.identity);
            rock.GetComponent<Rock>().target=attackTarget;
        }
    }
}
