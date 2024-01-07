using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDataManager
{
    //Singleton (probably wont be necessary)
    private static GameDataManager instance;
    public static GameDataManager Instance
    {
        get{ 
            if(instance == null) { instance = new GameDataManager(); } 
            return instance; 
        }
        private set { instance = value; }
    }

    //Delegates Declaration
    public delegate void OnCollectProp(IGrabbable value);
    public delegate void OnDestroyProp(IDestructable value);
    public delegate void OnGainScore(IValuable value);
    public delegate void OnTakeDamage(int currentHP, int maxHP);
    public delegate void OnPlayerDie();

    //Event delegates
    public OnCollectProp onCollectProp;
    public OnDestroyProp onDestroyProp;
    public OnGainScore onGainScore;
    public OnTakeDamage onTakeDamage;
    public OnPlayerDie onPlayerDie;


}
