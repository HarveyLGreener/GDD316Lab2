using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int hp = 1;
    public ObjectiveTracker tracker;

    public void Awake()
    {
        tracker = FindObjectOfType<ObjectiveTracker>();
    }
    public int OnClick(int type)
    {
        type++;
        hp--;
        if (hp <= 0) 
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject); 
        }
        return type;
    }
}
