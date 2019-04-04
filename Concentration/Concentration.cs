using System;
using System.Collections.Generic;

namespace Concentration
{
    public class Concentration
    {
        public int gameScore { get; set; } = 0;
        public int flipCount { get; set; } = 0;
        public bool gameOver { get; set; } = false;
        public int cardInGame { get; set; } = 0;
        List<Card> cards = new List<Card>();

        private int indeOfOneAndOnlyFaceUp
        {
            get
            {
                int? foundIndex = null;
                for(int i = 0; i < cards.Count; ++i)
                {
                    if(cards[i].isFaceUp)
                    {
                        if(foundIndex != null) 
                        {
                            foundIndex = i; 
                        }

                    }
                }
            }
        }

        public Concentration(int numberOfPairsOfCards)
        {
           for(int i = 0; i < numberOfPairsOfCards; ++i)
            {
                cards.Add(new Card());
                cards.Add(new Card());
            }

            //TO DO: Card Shuffle
            cardInGame = cards.Count;
        }



    }
}
