using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill03 : MonoBehaviour
{
    public EffectInfo[] Effects;
    //private Animator anim;

    [System.Serializable]

    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
    }
    void InstantiateEffect03(int EffectNumber)
    {
        if (Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);

        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        Destroy(instance, Effects[EffectNumber].DestroyAfter);
    }
}
