using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorBehavior :  BehaviorInterface
{
    
    protected Material defaultLevel;
    protected Material firstLevel;
    protected Material secondLevel;
    protected Material thirdLevel;

    public Material defaultSprite(){
        return defaultLevel;
    }
    public Material firstLevelSprite(){
        return firstLevel;
    }
    public Material secondLevelSprite(){
        return secondLevel;
    }
    public Material thirdLevelSprite(){
        return thirdLevel;
    }
    public bool isSame(BehaviorInterface behavior) {
        //Debug.Log(this.GetType().IsEquivalentTo(behavior.GetType()));
        return this.GetType().IsEquivalentTo(behavior.GetType());
    }
}
