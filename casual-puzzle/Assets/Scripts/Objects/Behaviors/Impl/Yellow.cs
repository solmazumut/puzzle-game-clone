using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Behaviors 
{
    public class Yellow :  Behaviors.Color
    {
        // Start is called before the first frame update
        public Yellow(ArrayList levelBoundaries)
        {
            this.initialMaterials = new ArrayList{ 
                Resources.Load<Material>("Materials/Yellow_D"), 
                Resources.Load<Material>("Materials/Yellow_A"),
                Resources.Load<Material>("Materials/Yellow_B"),
                Resources.Load<Material>("Materials/Yellow_C"),
            };

            SetInitialLevels(levelBoundaries);

        }
    }
}