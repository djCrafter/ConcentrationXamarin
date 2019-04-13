using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using ConcentrationModel;
using System.Linq;


namespace ConcentrationApp
{
    public partial class ViewController : UIViewController
    {

        private Random random = new Random();

        private Concentration game;
      
        private List<UIButton> cardButtons;
        private Dictionary<int, string> emoji = new Dictionary<int, string>(); 

        private List<string> emojiChoices;
        private List<List<string>> emojiSets = new List<List<string>>()
        {
            new List<string> { "🦇", "😱", "🙀", "😈", "🎃", "👻", "🍭", "🍬", "🍎" },
            new List<string> { "🐏", "🐖", "🐈", "🐇", "🦝", "🦏", "🦔", "🐎", "🐓" },
            new List<string> { "🍏", "🍋", "🍅", "🥭", "🥑", "🍆", "🥔", "🥥", "🍌" },
            new List<string> { "💻", "📱", "🖨", "💿", "☎️", "📺", "🎥", "⌚️", "⏰" },
            new List<string> { "♋️", "♒️", "♍️", "♓️", "♐️", "♏️", "♎️", "♈️", "♌️" },
            new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I" }
        };
      
    


        private void InitButtonsList()
        {
            cardButtons = new List<UIButton>();
            cardButtons.Add(card1);
            cardButtons.Add(card2);
            cardButtons.Add(card3);
            cardButtons.Add(card4);
            cardButtons.Add(card5);
            cardButtons.Add(card6);
            cardButtons.Add(card7);
            cardButtons.Add(card8);
            cardButtons.Add(card9);
            cardButtons.Add(card10);
            cardButtons.Add(card11);
            cardButtons.Add(card12);
            cardButtons.Add(card13);
            cardButtons.Add(card14);
            cardButtons.Add(card15);
            cardButtons.Add(card16);
        }


        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitButtonsList();
            StartNewGame();
        }




        private void StartNewGame()
        {
            emojiChoices = Clone(emojiSets[random.Next(emojiSets.Count)]);
            game = new Concentration((cardButtons.Count + 1) / 2);
            UpdateViewFromModel();
            gameOverLabel.TextColor = UIColor.Black;
        }
           

        private void UpdateViewFromModel()
        {
            for(int i = 0; i < cardButtons.Count; ++i)
            {
                UIButton button = cardButtons[i];
                Card card = game.cards[i];
                
                if(card.isFaceUp)
                {
                    button.SetTitle(Emoji(card), UIControlState.Normal);
                    button.BackgroundColor = UIColor.White;
                }
                else
                {
                    button.SetTitle("", UIControlState.Normal);
                    button.BackgroundColor = card.isMatched ? UIColor.Clear : UIColor.Orange;                                    
                }
            }
            flipCountLabel.Text = $"Flips: {game.flipCount}";
            scoreLabel.Text = $"Score: {game.gameScore}";
            if (game.gameOver)
            {
                gameOverLabel.TextColor = UIColor.Red;
            }
        }

        private string Emoji(Card card)
        {
            if (!emoji.ContainsKey(card.identifier) && emojiChoices.Count > 0)
            {
                int rand = random.Next(emojiChoices.Count);
                emoji[card.identifier] = emojiChoices[rand];
                emojiChoices.RemoveAt(rand);
            }
            return emoji.ContainsKey(card.identifier) ? emoji[card.identifier] : "?";
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);

            if (UIDevice.CurrentDevice.Orientation.IsLandscape())
            {
                flipScoreStack.Axis = UILayoutConstraintAxis.Horizontal;
            }
            else
            {
                flipScoreStack.Axis = UILayoutConstraintAxis.Vertical;
            }
        }

        private List<String> Clone(List<String> list)
        {
            var newList = new List<string>();
            foreach (var str in list)
            {
                newList.Add(str);
            }
            return newList;
        }



        partial void StartNewGameButton_TouchUpInside(UIButton sender)
        {
            StartNewGame();
        }

        partial void Card1_TouchUpInside(UIButton sender)
        {
            if (cardButtons.Contains(sender))
            {
                if (sender.BackgroundColor != UIColor.Clear)
                {
                    int cardNumber = cardButtons.IndexOf(sender);
                    game.chooseCard(cardNumber);
                                     
                    UpdateViewFromModel();
                }
            }
            else Console.WriteLine("choosen card was not in cardButtons");
        }



    }


}
