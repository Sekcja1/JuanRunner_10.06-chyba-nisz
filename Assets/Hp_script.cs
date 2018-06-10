using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_script : MonoBehaviour {

    Vector3 localscale;

	// Use this for initialization
	void Start () {
        localscale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (Char_move_test.healtAmount >= 0)
        {
            localscale.x = Char_move_test.healtAmount;
            transform.localScale = localscale;
        }
	}
}
