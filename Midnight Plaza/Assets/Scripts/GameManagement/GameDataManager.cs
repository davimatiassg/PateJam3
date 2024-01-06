using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDataManager : MonoBehaviour
{
    //Singleton (probably wont be necessary)
    private static instance;
    static GameDataManager Instance
    {
        get{ if(instance == null) { instance = new GameDataManager(); } return Instance; }
        private set{};
    }

    //Delegates to Events

    public static delegate void OnGrabProp(Person value);
    public static delegate void OnDestroyProp(IDestructable prop);

}
