using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this object should simply tell the deck to spawn the next
// spell x+1 more times than normal, where x is num lightning cards
public class QuickThinkingSpell : Spell
{
    public override void InitSpell()
    {
        int numLightning = Deck.instance.NumOfTypeInHand(CardType.Lightning);
        Deck.instance.numTimesToCast = numLightning;
        
        //print("Quick: numTimesToCast = " + Deck.instance.numTimesToCast);

        Destroy(gameObject);
    }
}
