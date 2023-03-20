
namespace FridayPartyCGOL
{
    public Class CardCollection {
        protected ArrayList<Card> cards;
        // protected Board activeBoard;
        public CardCollection(ArrayList<Card> startingCards) {

            this.cards = new ArrayList<Card>();
            cards = startingCards;
        }

        public void removeAllCards() {
            this.cards.clear();
        }

        public void shuffle() {
            Collections.shuffle(cards);
        }

        public Card removeFromTop() {
            if (cards.isEmpty()) {
                // throw new RuntimeException("no fucking cards left in this stack");
                return null;
            }
            return cards.remove(0);
        }

        public int size() {
            return cards.size();
        }

        public void addCard(Card card, boolean toBottom) {
            if (toBottom){
                cards.add(card);
            }
            else
            cards.add(0, card);
            
        }

        public void addCard(ArrayList<Card> cardsToAdd) {
            cards.addAll(0, cardsToAdd);
        }
        public void addCard(ArrayList<Card> cardsToAdd, boolean toBottom ) {       
            cards.addAll(0, cardsToAdd);
        }

        public void addStackTobottom(ArrayList<Card> cardsToAdd) {
            cards.addAll(cardsToAdd);
        }

        public void removeCardofType(Card card) {
            cards.remove(card);
        }

        public ArrayList<Card> GetFullStack() {
            return cards;
        }

        public ArrayList<Card> scryThis(int depth) {
            if (cards.size() < depth) {
                depth = cards.size();
            }
            return new ArrayList<>(cards.subList(0, depth));
        }

    }
}