using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
 
    protected override void OnRabitHit(HeroRabit rabit)
    {
        if (rabit.isBig == false)
        {
            rabit.isBig = true;
            rabit.transform.localScale = new Vector3(2, 2, 2); ;
        }
        
        this.CollectedHide();
    }
}