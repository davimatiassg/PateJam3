using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Person", menuName = "Person/new Person", order = 0)]
public class Person : ScriptableObject, IDestructable, IGrabable {
    public Texture2D sprite;
    
    public int ScoreValue { get; set; }

    public int getDestroyed()
    {
        return 0;
    }

    public void getGrabed()
    {
        return;
    }
}
