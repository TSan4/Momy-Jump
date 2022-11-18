using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MommyJump
{
    public class BreakablePlatf : Platform
    {
        public override void PlatformAction()
        {
            Destroy(gameObject);
        }
    }
}
