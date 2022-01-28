using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors 
{

    public interface IColor
    {
        Material GetMaterial(int levelNumber);
        bool IsSame(IColor color);
    }

}

