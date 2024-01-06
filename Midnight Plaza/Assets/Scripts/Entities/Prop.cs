using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehaviour : MonoBehaviour {
   
}


[CreateAssetMenu(fileName = "Scenary Prop", menuName = "Entities/new Scenary Prop", order = 0)]
public class Prop : ScriptableObject, IValuable {
   [SerializeField] private int scoreValue;
    public int ScoreValue { get {return this.scoreValue;}  set{this.scoreValue = value;} }
}
