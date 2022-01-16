using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBehavior :  ColorBehavior
{
        public PinkBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Pink_D");
        firstLevel = Resources.Load<Material>("Materials/Pink_A");
        secondLevel = Resources.Load<Material>("Materials/Pink_B");
        thirdLevel = Resources.Load<Material>("Materials/Pink_C");
    }

}
