using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Entities/new Building", order = 0)]
public class Building : ScriptableObject,  IValuable{

    public int hp;
    public GameObject structure;
    [SerializeField] private int scoreValue;
    public int ScoreValue { get {return this.scoreValue;}  set{this.scoreValue = value;} }
}
