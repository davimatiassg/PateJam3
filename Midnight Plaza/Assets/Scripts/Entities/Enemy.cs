using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Enemy", menuName = "Entities/new Enemy (caraca isso é muito legal)", order = 0)]
public class Enemy : ScriptableObject, IValuable {
    public Sprite sprite;
    public float atk;
    public float speed;
    //public Sprite worriedSprite;
    
    [SerializeField] private int scoreValue;
    public int ScoreValue { get {return this.scoreValue;}  set{this.scoreValue = value;} }

    public void getGrabbed(){return;}

    /*
    #if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {   
    }
    #endif
    */
}
