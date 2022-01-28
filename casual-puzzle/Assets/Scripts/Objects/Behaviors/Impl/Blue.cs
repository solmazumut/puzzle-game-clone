using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors 
{
    public class Blue :  Behaviors.Color
    {
        // Start is called before the first frame update
        public Blue(ArrayList levelBoundaries)
        {
            this.initialMaterials = new ArrayList{ 
                Resources.Load<Material>("Materials/Blue_D"), 
                Resources.Load<Material>("Materials/Blue_A"),
                Resources.Load<Material>("Materials/Blue_B"),
                Resources.Load<Material>("Materials/Blue_C"),
            };

            SetInitialLevels(levelBoundaries);

        }
    }
}