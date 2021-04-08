// all enums should be defined here 

// used in EnemyBase script
public enum StatusEffect
{
    Shock,  // double damage taken
    Ignite, // damage over time
    Freeze, // freezes enemy and deal % damage upon breaking
    Rot,    // apply damage whenever a card is discarded
    Slow,   // cuts movement speed in half 
}

public enum CardType
{
    Lightning,
    Frost,
    Shadow,
    Fire,
    NA
}

