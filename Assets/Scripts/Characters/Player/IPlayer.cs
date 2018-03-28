
public interface IPlayer  {
	ICharacter[] Monsters {
		get;
		set;
	}

	uint Money {
		get;
		set;
	}

	uint Score {
		get;
		set;
	}

	// TODO: List of skills (needs skill interface)
}
