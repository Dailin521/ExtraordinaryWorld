using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyController
{
   [Header("Skill")]
    public GameObject FirePoint;
    public GameObject[] Prefabs;
    private Vector3 direction;
    private Quaternion rotation;
    ParticleCollisionInstance particleCollisionInstance;
    public void FireShoot(){
        if(attackTarget!=null){
            particleCollisionInstance=Prefabs[0].GetComponent<ParticleCollisionInstance>();
            Instantiate(Prefabs[0], FirePoint.transform.position, FirePoint.transform.rotation);
            particleCollisionInstance.RotateToDirection(attackTarget,this.gameObject);
            //bullet.GetComponent<Rock>().target=attackTarget;
        }
    }
}
