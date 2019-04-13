using System;

namespace ConcentrationModel
{
    public class Card
    {
        public bool isFaceUp = false;
        public bool isMatched = false;
        public int identifier;

        private static int identifierFactory = 0;
      
        private static int getUniqueIdentifier()
        {

            return Card.identifierFactory;
        }


        public Card()
        {
            identifier = Card.getUniqueIdentifier();
        }

        private Card(Card card)
        {
            isFaceUp = card.isFaceUp;
            isMatched = card.isMatched;
            identifier = card.identifier;
        }

        public Card Clone()
        {
            identifierFactory += 1;
            return new Card(this);
        }
    }
}
