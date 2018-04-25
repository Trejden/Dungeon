using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour, ICharacter, IEnemy
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

    public Orc() {
        strength = Random.Range(15, 30);
        endurance = Random.Range(10, 25);
        speed = Random.Range(5, 12);
        mana = 0;
        maxHealth = (int)(endurance * 1.5 + strength * 0.5);
        health = maxHealth;
    }
}