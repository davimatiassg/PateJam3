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
    public delegate void OnCollectProp(IGrabable value);
    public delegate void OnDestroyProp(IDestructable prop);


    //Event delegates
    public OnCollectProp onCollectProp;
    public OnDestroyProp onDestroyProp;
}
