using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
	private bool moving;

    public static PlayerControler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private bool CanMove(float x, float y) {
        // TODO: raytrace the path to look for obstacles (will be easy whenever test level appears)
        return true;
    }
    public void Move(Direction dir) {
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
        
    }

    public void ResetPos()
    {
        GetComponent<Transform>().position = new Vector3(0, 0, 0);
    }
}
