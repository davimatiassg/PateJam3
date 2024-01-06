using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenary Prop", menuName = "Person/new Prop", order = 0)]
public class Prop : ScriptableObject, IValuable {
   public int ScoreValue { get; set; }
}
