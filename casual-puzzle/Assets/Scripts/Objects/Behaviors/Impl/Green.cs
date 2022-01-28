using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Behaviors 
{
    public class Green :  Behaviors.Color
    {
        // Start is called before the first frame update
        public Green(ArrayList levelBoundaries)
        {
            this.initialMaterials = new ArrayList{ 
                Resources.Load<Material>("Materials/Green_D"), 
                Resources.Load<Material>("Materials/Green_A"),
                Resources.Load<Material>("Materials/Green_B"),
                Resources.Load<Material>("Materials/Green_C"),
            };

            SetInitialLevels(levelBoundaries);

        }
    }
}
