/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public GameObject enemy;
    public float speed;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;
    private int damageEne;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    GameObject target;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
            transform.LookAt(GameManager.Instance.playerStates.transform);
            transform.Translate(Vector3.forward * 10);

    }
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else { instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal); }
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);
            }
        }
        Destroy(gameObject, DestroyTimeDelay + 0.5f);
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<CharacterStates>().TakeDamage(20,other.gameObject.GetComponent<CharacterStates>());
        }
    }
   public void RotateToDirection(GameObject target_E,GameObject selfe)
    {
        target = target_E;
        enemy=selfe;
    }
}
