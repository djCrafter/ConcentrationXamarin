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

        private int? indexOfOneAndOnlyFaceUp
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
                return foundIndex;
            }
            set
            {
                for (int i = 0; i < cards.Count; ++i)
                {
                    cards[i].isFaceUp = (i == value);
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


        public (int flips, int scores, bool isGameOver) chooseCard(int index) {
            flipCount += 1;
            if(!cards[index].isMatched)
            {
                int? matchIndex = indexOfOneAndOnlyFaceUp;
                if(matchIndex != null && matchIndex != index)
                {
                    bool flag = true;
                    if (cards[matchIndex ?? default(int)].identifier == cards[index].identifier)
                    {
                        cards[matchIndex ?? default(int)].isMatched = true;
                        gameScore += 2;
                        cardInGame -= 2;

                        if (cardInGame < 2) gameOver = true;

                        flag = false;
                    }

                    cards[index].isFaceUp = true;
                    if (flag) gameScore--;
                }
                else
                {
                    indexOfOneAndOnlyFaceUp = index;
                }
            }
  return (flipCount, gameScore, gameOver);
 } 
    }
}
