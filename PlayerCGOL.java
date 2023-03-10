package Classes;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import Classes.CardClasses.Copper;
import Classes.CardClasses.DomCard;
import Classes.CardClasses.Estate;

public class Player {
    // private int totalVP = 0;
    public int coins{private set;} = 0;
    private int actions = 1;
    private int coffers = 0;
    private int villagers = 0;
    private int buys = 1;
    private CardCollection exileMat;
    private CardCollection CardsInPlay;
    // private CardStack islandMat;
    // private CardStack tavernMat;
    // private CardStack playedThisTurn;
    // private CardStack forNextTurn;
    private CardCollection Deck;
    private CardCollection DiscardPile;
    private CardCollection CardsInHand;
    private final String name;

    public Player(String playerName) {
        this.name = playerName;
        this.Deck = new CardStack(new ArrayList<DomCard>());
        this.CardsInHand = new CardStack(new ArrayList<DomCard>());
        this.CardsInPlay = new CardStack(new ArrayList<DomCard>());
        this.exileMat = new CardStack(new ArrayList<DomCard>());
        this.DiscardPile = new CardStack(new ArrayList<DomCard>());
        for (int i = 0; i < 7; i++) {
            this.Deck.addCardToTop(new Copper());
        }
        for (int i = 0; i < 3; i++) {
            this.Deck.addCardToTop(new Estate());
        }
        this.Deck.shuffle();
        drawXcardsFrom_Deck(5);
    }


    // returns a ArrayList of cards where the card.Type contains the string "action"
    public ArrayList<DomCard> getActionsFromStack(CardStack stack) {
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
    public ArrayList<DomCard> getTreasureFromStack(CardStack stack) {
        ArrayList<DomCard> Treasurecards = new ArrayList<>();
        for (DomCard card : stack.GetFullStack()) {
            if (card.getType().contains("Treasure")) {
                Treasurecards.add(card);
            }
        }
        return Treasurecards;
    }

    public CardStack getDeck() {
        return this.Deck;
    }

    public void setCoins(int amount) {
        this.coins = amount;
    }

    public void setVillagers(int amount) {
        this.villagers = amount;
    }

    public void setBuys(int amount) {
        this.buys = amount;
    }

    public void setCoffers(int amount) {
        this.coffers = amount;
    }

    public void setActions(int amount) {
        this.actions = amount;
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

    public int getNumActions() {
        return this.actions;
    }

    public int getNumVillagers() {
        return this.villagers;
    }

    public int getNumBuys() {
        return this.buys;
    }

    public int getNumCoffers() {
        return this.coffers;
    }

    public int getNumCoins() {
        return this.coins;
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
            this.CardsInHand.addCardtoBottom(Deck.removeFromTop());
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
        this.Deck.addCardToTop(card);
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

    public DomCard chooseCardFromHand(String message, boolean isNotOptional) {
        DomCard selectedCard = null;
        if (CardsInHand.size() == 0) {
            System.out.println("no cards in hand, choice = null");
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

    public DomCard chooseAction_Hand(String message, boolean isNotOptional) {
        DomCard selectedCard = null;
        if (getActionsFromStack(CardsInHand).size() == 0) {
            System.out.println("no Action cards in hand, choice = null");
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

    public DomCard chooseTreasure_Hand(String message, boolean isNotOptional) {
        DomCard selectedCard = null;
        if (getTreasureFromStack(CardsInHand).size() == 0) {
            System.out.println("no Treasure cards in hand, choice = null");
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
        this.DiscardPile.addCardToTop(Deck.removeFromTop());
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

    public void gainCard(DomCard card) {
        this.DiscardPile.addCardToTop(card);
    }

    public void gainToHand(DomCard chosenCard) {
        this.CardsInHand.addCardtoBottom(chosenCard);
    }

    public void gainToDeck(DomCard chosenCard) {
        this.Deck.addCardToTop(chosenCard);
    }

    public DomCard removeTopDiscard() {
        return this.DiscardPile.removeFromTop();
    }

    public void prettyPrintAll() {
        System.out.println("\nPlayer: " + this.name);
        System.out.print("Exile: ");
        if (this.exileMat.size() == 0) {
            System.out.print("empty  \n");
        } else {
            this.exileMat.prettyPrint();
        }
        System.out.println("Draw Pile: " + this.Deck.size() + " cards");
        System.out.println("Discard Pile: " + DiscardPile.size() + " cards");
        System.out.println("Actions:" + this.actions + " - Coins:" + this.coins + " - Coffers:" + this.coffers
                + " - Villagers:" + this.villagers);
        System.out.println("Hand: ");
        this.CardsInHand.prettyPrint();
    }

    public void prettyPrintNoHand() {
        System.out.println("Player: " + this.name);
        System.out.println("Exile: ");
        this.exileMat.prettyPrint();
        System.out.println("Draw Pile: " + this.Deck.size() + " cards");
        System.out.println("Discard Pile: " + DiscardPile.size() + " cards");
        System.out.println("Actions:" + this.actions + " - Coins:" + this.coins + " - Coffers:" + this.coffers
                + " - Villagers:" + this.villagers);
    }

    public DomCard promptPlayAction() {
        System.out.println("\nPlay Action Cards? type n to skip");
        return chooseAction_Hand("Play Action: ", false);
    }

    public DomCard promptPlayTreasure() {
        System.out.println("\nPlay Treasure Cards? type n to skip");
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
        CardsInPlay.addCardToTop(cardToPlay);
        cardToPlay.play(this);
    }
}
