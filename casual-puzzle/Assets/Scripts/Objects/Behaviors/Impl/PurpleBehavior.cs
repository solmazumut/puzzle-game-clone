using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBehavior :  ColorBehavior
{
        public PurpleBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Purple_D");
        firstLevel = Resources.Load<Material>("Materials/Purple_A");
        secondLevel = Resources.Load<Material>("Materials/Purple_B");
        thirdLevel = Resources.Load<Material>("Materials/Purple_C");
    }

}
