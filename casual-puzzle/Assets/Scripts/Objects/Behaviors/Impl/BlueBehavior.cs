using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBehavior :  ColorBehavior
{
    // Start is called before the first frame update
    public BlueBehavior()
    {
        defaultLevel = Resources.Load<Material>("Materials/Blue_D");
        firstLevel = Resources.Load<Material>("Materials/Blue_A");
        secondLevel = Resources.Load<Material>("Materials/Blue_B");
        thirdLevel = Resources.Load<Material>("Materials/Blue_C");
    }
}
