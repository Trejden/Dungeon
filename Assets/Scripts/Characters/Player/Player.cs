using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter, IPlayer
{
    public int Health {
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

    public bool IsDead {
        get;
        set;
    }

    public bool IsPlayer {
        get {return true;}
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

    public void Damage(int val) {
        Health -= val;
        if (Health <= 0) {
            IsDead = true;
        }
    }

    void LevelUp()
    {

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}
