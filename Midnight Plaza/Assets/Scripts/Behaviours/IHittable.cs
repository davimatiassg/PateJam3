using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void TakeDmg(float dmg, Vector3 force, GameObject source);
}
