using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter, IPlayer {
    public int Health
    {
        get;
        set;
    }

   public int MaxHealth {
        get;
        set;
    }

    public int Speed {
        get;
        set;
    }

    public int Mana {
        get;
        set;
    }

    public int Strength {
        get;
        set;
    }

    public int Endurance {
        get;
        set;
    }

    public ICharacter[] Monsters {
		get;
		set;
	}

	public uint Money {
		get;
		set;
	}

	public uint Score {
		get;
		set;
	}

    void LevelUp() {
        
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
