using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour, ICharacter, IEnemy
{
    private int health;
    private int maxHealth;
    private int speed;
    private int mana;
    private int strength;
    private int endurance;

    public int Health {
        get {return health;}
    }

    public int MaxHealth {
        get {return maxHealth;}
    }

    public int Speed {
        get {return speed;}
    }

    public int Mana {
        get {return mana;}
    }

    public int Strength {
        get {return strength;}
    }

    public int Endurance {
        get {return endurance;}
    }

    public Dwarf() {
        strength = Random.Range(15, 25);
        endurance = Random.Range(10, 20);
        speed = Random.Range(8, 15);
        mana = Random.Range(0, 10);
        maxHealth = (int)(endurance * 1.5 + strength * 0.5);
        health = maxHealth;
    }
}