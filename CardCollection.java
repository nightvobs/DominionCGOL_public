package Classes;

import java.util.ArrayList;
import java.util.Collections;
// import java.util.List;

// import Classes.BordSetupClasses.Board;
import Classes.CardClasses.DomCard;

public Class CardCollection {
    protected ArrayList<DomCard> cards;
    // protected Board activeBoard;
    public CardStack(ArrayList<DomCard> startingCards) {

        this.cards = new ArrayList<DomCard>();
        cards = startingCards;
    }

    public void removeAllCards() {
        this.cards.clear();
    }

    public void shuffle() {
        Collections.shuffle(cards);
    }

    public DomCard removeFromTop() {
        if (cards.isEmpty()) {
            // throw new RuntimeException("no fucking cards left in this stack");
            return null;
        }
        return cards.remove(0);
    }

    public int size() {
        return cards.size();
    }

    public void addCard(DomCard card, boolean toBottom) {
        if (toBottom){
            cards.add(card);
        }
        else
        cards.add(0, card);
        
    }

    public void addCard(ArrayList<DomCard> cardsToAdd) {
        cards.addAll(0, cardsToAdd);
    }
    public void addCard(ArrayList<DomCard> cardsToAdd, boolean toBottom ) {       
        cards.addAll(0, cardsToAdd);
    }

    public void addStackTobottom(ArrayList<DomCard> cardsToAdd) {
        cards.addAll(cardsToAdd);
    }

    public void removeCardofType(DomCard card) {
        cards.remove(card);
    }

    public ArrayList<DomCard> GetFullStack() {
        return cards;
    }

    public ArrayList<DomCard> scryThis(int depth) {
        if (cards.size() < depth) {
            depth = cards.size();
        }
        return new ArrayList<>(cards.subList(0, depth));
    }

}
