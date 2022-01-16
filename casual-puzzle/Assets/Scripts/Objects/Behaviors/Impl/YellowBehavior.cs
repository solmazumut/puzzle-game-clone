using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YellowBehavior :  ColorBehavior
{
    public YellowBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Yellow_D");
        firstLevel = Resources.Load<Material>("Materials/Yellow_A");
        secondLevel = Resources.Load<Material>("Materials/Yellow_B");
        thirdLevel = Resources.Load<Material>("Materials/Yellow_C");
    }
}

