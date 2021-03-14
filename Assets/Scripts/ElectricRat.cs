using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricRat : Character
{
    public override void StartMoveSelect()
    {
        // Would possibly add behavior tree to help determine battle behavior
        ServiceLocater.bm.AddToMoveQueue(new Attack(this, ServiceLocater.bm.player));
    }
}
