using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour {

    Text hpfield;
    ball ballScript;
    // Use this for initialization
    void Start () {
		hpfield = GetComponent<Text>();
        GameObject ballGo = GameObject.Find("ball");
        ballScript = (ball)ballGo.GetComponent(typeof(ball));
        hpfield.text = "HP: " + ballScript.getHp();
    }
	
	// Update is called once per frame
	void Update () {
        hpfield.text = "HP: " + ballScript.getHp();
    }
}
