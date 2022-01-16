using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviorInterface 
{
    Material defaultSprite();
    Material firstLevelSprite();
    Material secondLevelSprite();
    Material thirdLevelSprite();

    bool isSame(BehaviorInterface behavior);
}
