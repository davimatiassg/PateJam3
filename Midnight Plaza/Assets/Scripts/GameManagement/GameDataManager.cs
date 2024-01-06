using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDataManager : MonoBehaviour
{
    //Singleton (probably wont be necessary)
    private static GameDataManager instance;
    public static GameDataManager Instance
    {
        get{ if(instance == null) { instance = new GameDataManager(); } return Instance; }
        private set { instance = value; }
    }

    //Delegates Declaration
    public delegate void OnCollectProp(IGrabable value);
    public delegate void OnDestroyProp(IDestructable prop);


    //Static delegates
    public static OnCollectProp onCollectProp;
    public static OnDestroyProp onDestroyProp;
}
