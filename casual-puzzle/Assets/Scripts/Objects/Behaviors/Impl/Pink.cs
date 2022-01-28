using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviors 
{
    public class Pink :  Behaviors.Color
    {
        // Start is called before the first frame update
        public Pink(ArrayList levelBoundaries)
        {
            this.initialMaterials = new ArrayList{ 
                Resources.Load<Material>("Materials/Pink_D"), 
                Resources.Load<Material>("Materials/Pink_A"),
                Resources.Load<Material>("Materials/Pink_B"),
                Resources.Load<Material>("Materials/Pink_C"),
            };

            SetInitialLevels(levelBoundaries);

        }
    }
}
