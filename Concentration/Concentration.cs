using System;
using System.Collections.Generic;

namespace ConcentrationModel
{
    static class MyExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
   

    public class Concentration
    {

        private int matches = 0;

        public int gameScore { get; private set; } = 0;
        public int flipCount { get; private set; } = 0;
        public bool gameOver { get; private set; } = false;
        public int cardInGame { get;  private set; } = 0;
        public List<Card> cards = new List<Card>();

   

        private int? indexOfOneAndOnlyFaceUp
        {
            get
            {
                int? foundIndex = null;
                for(int i = 0; i < cards.Count; ++i)
                {
                    if(cards[i].isFaceUp)
                    {                    
                        foundIndex = i; 
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
                var card = new Card();
                cards.Add(card);
                cards.Add(card.Clone());
            }

          //  MyExtension.Shuffle(cards);
            cardInGame = cards.Count;
        }



        public void chooseCard(int index) {
            if (!gameOver)
            {
                flipCount += 1;
                gameScore--;
                if (!cards[index].isMatched)
                {
                    int? matchIndex = indexOfOneAndOnlyFaceUp;
                    matches++;
                    if (matchIndex != null && matchIndex != index)
                    {
                        if (cards[matchIndex ?? default(int)].identifier == cards[index].identifier)
                        {
                            cards[matchIndex ?? default(int)].isMatched = true;
                            cards[index].isMatched = true;

                            gameScore += 5;
                            cardInGame -= 2;

                            if (cardInGame < 2) gameOver = true;
                        }
                        else
                        {
                            if (matches > 1)
                            {
                                matches = 0;
                                indexOfOneAndOnlyFaceUp = null;
                            }
                        }
                        cards[index].isFaceUp = true;
                    }
                    else
                    {
                        indexOfOneAndOnlyFaceUp = index;
                    }
                }
            }
 } 
    }
}
