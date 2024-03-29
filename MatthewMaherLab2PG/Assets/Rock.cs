using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Collectible
{
    // Start is called before the first frame update
    public void OnHit()
    {
        tracker.rock = OnClick(tracker.rock);
    }
}
