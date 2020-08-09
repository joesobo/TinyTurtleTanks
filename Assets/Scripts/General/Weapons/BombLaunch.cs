using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLaunch : MonoBehaviour {
    public int speed;
    public int decaySpeed;
    public int damage;

    private Rigidbody bomb_rb;
    private Rigidbody player_rb;

    private void Start() {
        bomb_rb = GetComponent<Rigidbody>();
    }

    public void launch() {
        if (!bomb_rb) {
            bomb_rb = GetComponent<Rigidbody>();
        }
        // if (!player_rb) {
        //     player_rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        // }
        //bomb_rb = player_rb;
        bomb_rb.AddForce(transform.up * speed);
        bomb_rb.AddForce(transform.forward * speed);
    }
}
