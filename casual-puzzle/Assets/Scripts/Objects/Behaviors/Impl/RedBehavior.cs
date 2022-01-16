using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBehavior :  ColorBehavior
{
       public RedBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Red_D");
        firstLevel = Resources.Load<Material>("Materials/Red_A");
        secondLevel = Resources.Load<Material>("Materials/Red_B");
        thirdLevel = Resources.Load<Material>("Materials/Red_C");
    }

}

