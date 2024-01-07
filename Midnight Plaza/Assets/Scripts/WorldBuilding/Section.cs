using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    [SerializeField] private List<SubSection> subsections = new List<SubSection>();

    private void Start() {
        foreach(SubSection s in subsections)
        {
            s.Structure = SectionData.GetRandomBuilding();
        }
    }
}
