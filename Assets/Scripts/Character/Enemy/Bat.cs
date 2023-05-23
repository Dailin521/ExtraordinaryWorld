using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    [Header("Skill")]
    public GameObject FirePoint;
    public GameObject[] Prefabs;
    private Vector3 direction;
    private Quaternion rotation;
   // ParticleCollisionInstance particleCollisionInstance;
    public void FireShoot(){
        if(attackTarget!=null){
            Instantiate(Prefabs[0], FirePoint.transform.position, FirePoint.transform.rotation);
            //particleCollisionInstance=Prefabs[0].GetComponent<ParticleCollisionInstance>();
            //Debug.Log(attackTarget.name);
            //particleCollisionInstance.RotateToDirection(attackTarget,this.gameObject);
            //bullet.GetComponent<Rock>().target=attackTarget;
        }
    }
    // void RotateToDirection (GameObject obj, Vector3 destination)
    // {
    //     direction = destination - obj.transform.position;
    //     rotation = Quaternion.LookRotation(direction);
    //     obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    // }
}
