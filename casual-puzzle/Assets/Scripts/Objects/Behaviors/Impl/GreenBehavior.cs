using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBehavior :  ColorBehavior
{
    public GreenBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Green_D");
        firstLevel = Resources.Load<Material>("Materials/Green_A");
        secondLevel = Resources.Load<Material>("Materials/Green_B");
        thirdLevel = Resources.Load<Material>("Materials/Green_C");
    }
}
