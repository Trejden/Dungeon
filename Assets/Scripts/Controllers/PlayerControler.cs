using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Other;

public class PlayerControler : MonoBehaviour {
	public float speed = 3f;
	private Vector2 target;

	public void ResetPos()
    {
        GetComponent<Transform>().position = new Vector3(0, 0, 0);
    }

	// Use this for initialization
	void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	void FixedUpdate() {
        
    }

	void OnCollisionStay2D(Collision2D col) {
		Debug.Log(col.transform.position);
		var pos = transform.position;
		if (col.transform.position.x == 0f) {
			if (pos.y > 0f) {
				pos.y = col.transform.position.y - 0.5f;
			} else {
				pos.y = col.transform.position.y + 0.5f;
			}
			target.y = pos.y;
		}
		if (col.transform.position.y == 0f) {
			if (pos.x > 0f) {
				pos.x = col.transform.position.x - 0.4f;
			} else {
				pos.x = col.transform.position.x + 0.4f;
			}
			target.x = pos.x;
		}
		transform.position = pos;
	}
}
