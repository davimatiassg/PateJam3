using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerHit : MonoBehaviour
{
    private PlayerBehaviour player;
    private void Awake() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other) {
       
        other.gameObject.TryGetComponent(out IHittable victim);
        if(victim == null){ return; }
        victim.TakeDmg(player.atk, transform.forward*player.atk/2, this.gameObject);
    }
}
