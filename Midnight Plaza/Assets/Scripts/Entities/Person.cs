using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Person", menuName = "Person/new Person", order = 0)]
public class Person : ValuableObject, IDestructable, IGrabable {
    public Texture2D sprite;
}
