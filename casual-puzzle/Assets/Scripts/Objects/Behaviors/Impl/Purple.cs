using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors 
{
    public class Purple :  Behaviors.Color
    {
        // Start is called before the first frame update
        public Purple(ArrayList levelBoundaries)
        {
            this.initialMaterials = new ArrayList{ 
                Resources.Load<Material>("Materials/Purple_D"), 
                Resources.Load<Material>("Materials/Purple_A"),
                Resources.Load<Material>("Materials/Purple_B"),
                Resources.Load<Material>("Materials/Purple_C"),
            };

            SetInitialLevels(levelBoundaries);

        }
    }
}

