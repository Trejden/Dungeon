
public interface ICharacter {
	int Health {
        get;
    }

    int MaxHealth {
        get;
    }

    int Speed {
        get;
    }

    int Mana {
        get;
    }

    int Strength {
        get;
    }

    int Endurance {
        get;
    }

    bool IsDead {
        get;
    }

    bool IsPlayer{
        get;
    }

    void Damage(int val);

    // TODO: List of spells (needs spell interface)
    // TODO: List of items (needs item interface)


}
