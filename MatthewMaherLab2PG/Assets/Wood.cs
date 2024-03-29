using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Collectible
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

    }

    public void OnHit()
    {
        tracker.wood = OnClick(tracker.wood);
    }
}
