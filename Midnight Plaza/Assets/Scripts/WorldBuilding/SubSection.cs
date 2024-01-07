using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSection : MonoBehaviour
{
    [SerializeField] private GameObject structure;
    [SerializeField] public GameObject Structure{ 
        get { return structure; } 
        set{
            if(value == null) { this.structure = null; return; }
            if(this.structure != null){ Destroy(this.structure); }
            this.structure = GameObject.Instantiate(value, this.transform);
        } 
    }
}
