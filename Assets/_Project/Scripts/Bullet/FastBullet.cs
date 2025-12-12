using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBullet : Bullet
{
    private void Awake()
    {
        SetBullet((int)0.5, 3.5f);
    }
}
