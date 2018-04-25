using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
	public float MovementSpeed = 3f;
	private Vector2 Target;
	public static PlayerControler Instance;
    public bool CanUseDoor { get { return !GameController.Instance.IsNextRoomMoving; } }

	public void ResetPos()
    {
        GetComponent<Transform>().position = new Vector3(0, 0, 0);
    }

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && !GameController.Instance.IsNextRoomMoving) {
			Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector2.MoveTowards(transform.position, Target, MovementSpeed * Time.deltaTime);
	}

	void FixedUpdate() {
        
    }

	void OnCollisionStay2D(Collision2D col) {
		Debug.Log(col.transform.position);
		var pos = transform.position;
		if (col.transform.position.x == 0f) {
			if (pos.y > 0f) {
				pos.y = col.transform.position.y - 1.8f;
			} else {
				pos.y = col.transform.position.y + 1.8f;
			}
			Target.y = pos.y;
		}
		if (col.transform.position.y == 0f) {
			if (pos.x > 0f) {
				pos.x = col.transform.position.x - 1.6f;
			} else {
				pos.x = col.transform.position.x + 1.6f;
			}
			Target.x = pos.x;
		}
		transform.position = pos;
	}
}
