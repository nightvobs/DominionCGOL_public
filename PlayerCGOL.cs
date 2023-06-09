
namespace FridayPartyCGOL
{
    public class Player {
        // private int totalVP = 0;
        private int coins{public get; public set;} = 0;
        private int actions{public get; public set;} = 1;
        private int coffers{public get; public set;} = 0;
        private int villagers{public get; public set;} = 0;
        private int buys{public get; public set;} = 1;
        private CardCollection exileMat;
        private CardCollection CardsInPlay;
        // private CardCollection islandMat;
        // private CardCollection tavernMat;
        // private CardCollection playedThisTurn;
        // private CardCollection forNextTurn;
        private CardCollection Deck{public get;}
        private CardCollection DiscardPile;
        private CardCollection CardsInHand;
        private final string name;

        public Player(string playerName) {
            this.name = playerName;
            this.Deck = new CardCollection(new ArrayList<DomCard>());
            this.CardsInHand = new CardCollection(new ArrayList<DomCard>());
            this.CardsInPlay = new CardCollection(new ArrayList<DomCard>());
            this.exileMat = new CardCollection(new ArrayList<DomCard>());
            this.DiscardPile = new CardCollection(new ArrayList<DomCard>());
            for (int i = 0; i < 7; i++) {
                this.Deck.addCard(new Copper());
            }
            for (int i = 0; i < 3; i++) {
                this.Deck.addCard(new Estate());
            }
            this.Deck.shuffle();
            drawXcardsFrom_Deck(5);
        }


        // returns a ArrayList of cards where the card.Type contains the string "action"
        public ArrayList<DomCard> getActionsFromStack(CardCollection stack) {
            ArrayList<DomCard> actionCards = new ArrayList<>();
            for (DomCard card : stack.GetFullStack) {
                if (card.Type.contains("Action")) {
                    actionCards.add(card);
                }
            }
            return actionCards;
        }

        // returns a ArrayList of cards where the card.Type contains the string
        // "Treasure"
        public ArrayList<DomCard> getTreasureFromStack(CardCollection stack) {
            ArrayList<DomCard> Treasurecards = new ArrayList<>();
            for (DomCard card : stack.GetFullStack()) {
                if (card.getType().contains("Treasure")) {
                    Treasurecards.add(card);
                }
            }
            return Treasurecards;
        }

        public CardCollection getDeck() {
            return this.Deck;
        }

        public void addCoins(int amount) {
            this.coins += amount;
        }

        public void addVillagers(int amount) {
            this.villagers += amount;
        }

        public void addBuys(int amount) {
            this.buys += amount;
        }

        public void addCoffers(int amount) {
            this.coffers += amount;
        }

        public void addActions(int amount) {
            this.actions += amount;
        }

        // public void addVP(int amount) {
        // this.totalVP += amount;
        // }

        public void shuffleDiscPileToDeck() {
            this.DiscardPile.shuffle();
            this.Deck.addStackTobottom(DiscardPile.GetFullStack());
            this.DiscardPile.removeAllCards();
        }

        public void drawFromDeck() {
            if (this.Deck.size() > 0) {
                this.CardsInHand.addCard(Deck.removeFromTop(), true);
            } else if (this.DiscardPile.size() > 0) {
                shuffleDiscPileToDeck();
                drawFromDeck();
            }
        }

        public void drawXcardsFrom_Deck(int amount) {
            for (int i = 0; i < amount; i++) {
                drawFromDeck();
            }
        }

        public void topDeckCard(DomCard card) {
            this.Deck.addCard(card);
        }

        // public void countAllVP() {

        // this.totalVP = 2;
        // }

        public ArrayList<DomCard> scryDeck(int depth) {
            if (Deck.size() >= depth) {
            } else if (DiscardPile.size() > 0) {
                shuffleDiscPileToDeck();
                return new ArrayList<DomCard>(Deck.GetFullStack().subList(0, depth));
            } else {
                depth = Deck.GetFullStack().size();
            }

            return new ArrayList<DomCard>(Deck.GetFullStack().subList(0, depth));
        }

        //

        public DomCard chooseCardFromHand(string message, boolean isNotOptional) {
            DomCard selectedCard = null;
            if (CardsInHand.size() == 0) {
                console.writeln("no cards in hand, choice = null");
                return null;
            } else {
                Map<DomCard, Integer> Avail_Selection = new HashMap<DomCard, Integer>();
                for (DomCard card : CardsInHand.GetFullStack()) {
                    Avail_Selection.put(card, 1);
                }
                selectedCard = globalMethods.chooseCard_Selection(message, Avail_Selection, isNotOptional);
            }
            return selectedCard;
        }

        public DomCard chooseAction_Hand(string message, boolean isNotOptional) {
            DomCard selectedCard = null;
            if (getActionsFromStack(CardsInHand).size() == 0) {
                console.writeln("no Action cards in hand, choice = null");
                return null;
            } else {
                Map<DomCard, Integer> Avail_Selection = new HashMap<DomCard, Integer>();
                for (DomCard card : getActionsFromStack(this.CardsInHand)) {
                    Avail_Selection.put(card, 1);
                }
                selectedCard = globalMethods.chooseCard_Selection(message, Avail_Selection, isNotOptional);
            }
            return selectedCard;
        }

        public DomCard chooseTreasure_Hand(string message, boolean isNotOptional) {
            DomCard selectedCard = null;
            if (getTreasureFromStack(CardsInHand).size() == 0) {
                console.writeln("no Treasure cards in hand, choice = null");
                return null;
            } else {
                Map<DomCard, Integer> Avail_Selection = new HashMap<DomCard, Integer>();
                for (DomCard card : getTreasureFromStack(this.CardsInHand)) {
                    Avail_Selection.put(card, 1);
                }
                selectedCard = globalMethods.chooseCard_Selection(message, Avail_Selection, isNotOptional);
            }
            return selectedCard;
        }

        public DomCard removeCardFrom_Discard(DomCard card) {
            this.DiscardPile.removeCardofType(card);
            return card;
        }

        public DomCard DiscardFromTopDeck() {
            this.DiscardPile.addCard(Deck.removeFromTop());
            return this.DiscardPile.scryThis(1).get(0);

        }

        public DomCard removeCardFrom_Exile(DomCard card) {
            this.exileMat.removeCardofType(card);
            return card;
        }

        public DomCard removeCardFrom_Hand(DomCard card) {
            this.CardsInHand.removeCardofType(card);
            return card;
        }

        public void gainCardtocollection(DomCard card, string collection) {
            this[collection].addCard(card); //works for DiscardPile, CardsInHand and Deck
        }

        public DomCard removeTopDiscard() {
            return this.DiscardPile.removeFromTop();
        }

        public void prettyPrintAll() {
            console.writeln("\nPlayer: " + this.name);
            console.writeln("Exile: ");
            if (this.exileMat.size() == 0) {
                console.writeln("empty  \n");
            } else {
                this.exileMat.prettyPrint();
            }
            console.writeln("Draw Pile: " + this.Deck.size() + " cards");
            console.writeln("Discard Pile: " + DiscardPile.size() + " cards");
            console.writeln("Actions:" + this.actions + " - Coins:" + this.coins + " - Coffers:" + this.coffers
                    + " - Villagers:" + this.villagers);
            console.writeln("Hand: ");
            this.CardsInHand.prettyPrint();
        }

        public void prettyPrintNoHand() {
            console.writeln("Player: " + this.name);
            console.writeln("Exile: ");
            this.exileMat.prettyPrint();
            console.writeln("Draw Pile: " + this.Deck.size() + " cards");
            console.writeln("Discard Pile: " + DiscardPile.size() + " cards");
            console.writeln("Actions:" + this.actions + " - Coins:" + this.coins + " - Coffers:" + this.coffers
                    + " - Villagers:" + this.villagers);
        }

        public DomCard promptPlayAction() {
            console.prompt("\nPlay Action Cards? type n to skip");
            return chooseAction_Hand("Play Action: ", false);
        }

        public DomCard promptPlayTreasure() {
            console.prompt("\nPlay Treasure Cards? type n to skip");
            return chooseTreasure_Hand("Play Treasure: ", false);
        }

        public void begginningTurn() {
            this.coins = 0;
            this.actions = 1;
            this.buys = 1;
        }

        public void cleanUp() {
            this.DiscardPile.addStackToTop(this.CardsInPlay.GetFullStack());
            this.CardsInPlay.removeAllCards();
            this.DiscardPile.addStackToTop(this.CardsInHand.GetFullStack());
            this.CardsInHand.removeAllCards();
            this.coins = 0;
            this.actions = 0;
            this.buys = 0;
        }

        public void playCard(DomCard cardToPlay) {
            removeCardFrom_Hand(cardToPlay);
            CardsInPlay.addCard(cardToPlay);
            cardToPlay.play(this);
        }
    }
}