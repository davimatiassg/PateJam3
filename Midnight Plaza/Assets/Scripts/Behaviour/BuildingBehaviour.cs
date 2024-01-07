using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour, ITakeDmg
{
    public float hp = 3f;
    
    private float r_die = 1.5f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            // delay before dying
            if (r_die <= 0) {Destroy(this.gameObject);}
            r_die -= Time.deltaTime;
        }
    }

    public void TakeDmg(float dmg, Vector3 force) {
        if (hp <= 0) {return;}

        hp -= dmg;

        // Team Rocket
        if (hp <= 0) {
            rb.isKinematic = false;
            rb.velocity += new Vector3(force.x, 20f, force.z);
            rb.AddTorque(new Vector3(20f, 10f, 20f) * 50f);
        }
    }
}
