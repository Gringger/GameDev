using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        
        if (rabit.isBig == false)
        {
            rabit.Die();
          
        }
        else
        {
            rabit.transform.localScale = new Vector3(1, 1, 1);
            rabit.timer = 10.0f;
            rabit.isBig = false;
        }
        this.CollectedHide();
    }
}
    

