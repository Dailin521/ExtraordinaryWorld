using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroy : Singleton<Dontdestroy>
{
   protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
