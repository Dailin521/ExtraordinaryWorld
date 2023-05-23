using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Basic Settings")]
    public int damage = 20;
    public float force;
    public GameObject target;
    public GameObject breakEffect;
    private Vector3 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.one;
        FlyToTarget();
    }
    // private void FixedUpdate() {
    //     if(rb.velocity.sqrMagnitude<0.1f){
    //         rockStates=RockStates.HitNothing;
    //     }
    // }
    public void FlyToTarget()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
        direction = (target.transform.position - transform.position + Vector3.up).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<NavMeshAgent>().isStopped=true;
            // other.gameObject.GetComponent<NavMeshAgent>().velocity=direction*force;
            // other.gameObject.GetComponent<Animator>().SetTrigger("Dizzy");
            other.gameObject.GetComponent<CharacterStates>().TakeDamage(damage, other.gameObject.GetComponent<CharacterStates>());
            Instantiate(breakEffect, transform.position, Quaternion.identity);
            Destroy(gameObject,2f);
        }
    }


}
