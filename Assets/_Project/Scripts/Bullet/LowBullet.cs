using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBullet : Bullet
{
    private void Awake()
    {
        SetBullet((int)1.5, 10f);
    }
    
}
