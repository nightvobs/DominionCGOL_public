
namespace FridayPartyCGOL
{
    public class Supply {

        private final ArrayList<DomCard> kingdomCards;

        // private final List<DomCard> baseCards;
        private Map<DomCard, Integer> cardsInSupplyMap;

        public Supply(ArrayList<DomCard> kingdomCards, int numPlayers, Boolean Prosperity) {
            this.kingdomCards = kingdomCards;
            this.cardsInSupplyMap = new HashMap<DomCard, Integer>();
            this.cardsInSupplyMap = initSupply(numPlayers, Prosperity);

        }

        private Map<DomCard, Integer> initSupply(int numberOfPlayers, boolean Prosperity) {
            // Add the BaseCards to the supply
            Map<DomCard, Integer> initialsupply = new HashMap<>();
            initialsupply.put(new Copper(), 60);
            initialsupply.put(new Silver(), 40);
            initialsupply.put(new Gold(), 30);
            if (Prosperity) {
                initialsupply.put(new Platinum(), (4 * numberOfPlayers - 4));
            }
            initialsupply.put(new Curse(), 10 * (numberOfPlayers - 1));
            switch (numberOfPlayers) {
                case 1:
                case 2:
                    initialsupply.put(new Estate(), 8);
                    initialsupply.put(new Duchy(), 8);
                    initialsupply.put(new Province(), 8);
                    if (Prosperity) {
                        initialsupply.put(new Colony(), 8);
                    }
                    break;
                case 3:
                case 4:
                    initialsupply.put(new Estate(), 12);
                    initialsupply.put(new Duchy(), 12);
                    initialsupply.put(new Province(), 12);
                    if (Prosperity) {
                        initialsupply.put(new Colony(), 12);
                    }
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    initialsupply.put(new Estate(), 4 * numberOfPlayers - 4);
                    initialsupply.put(new Duchy(), 4 * numberOfPlayers - 4);
                    initialsupply.put(new Province(), 4 * numberOfPlayers - 4);
                    if (Prosperity) {
                        initialsupply.put(new Colony(), 4 * numberOfPlayers - 4);
                    }
            }
            // Add the KingdomCards to the supply
            for (DomCard card : kingdomCards) {
                initialsupply.put(card, 10);
            }
            return initialsupply;
        }

        public void addCardsToSupply(DomCard cardName, int numCopies) {
            this.cardsInSupplyMap.put(cardName, cardsInSupplyMap.getOrDefault(cardName, 0) + numCopies);
        }

        public void removeCardsFromSupply(DomCard cardName, int numCopies) {
            int currentCount = cardsInSupplyMap.getOrDefault(cardName, 0);
            if (currentCount - numCopies < 0) {
                throw new IllegalArgumentException("Not enough " + cardName.toString() + " cards in supply!");
            }
            this.cardsInSupplyMap.put(cardName, currentCount - numCopies);
        }

        public int getNumCards(DomCard card) {
            return this.cardsInSupplyMap.getOrDefault(card, 0);
        }

        public Map<DomCard, Integer> getCardMap() {

            return this.cardsInSupplyMap;
        }

        public List<DomCard> getCardTypes() {
            return this.kingdomCards;
        }

        public void prettyPrint() {
            // Print Victory cards
            console.writeln("\nVictory Cards: ");
            for (DomCard card : cardsInSupplyMap.keySet()) {
                if (card.getType().contains("Victory")) {
                    console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");
                }
            }

            // Print Treasure cards
            console.writeln("\nTreasure Cards: ");
            for (DomCard card : cardsInSupplyMap.keySet()) {
                if (card.getName().contains("Copper")) {
                    console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");
                }
                if (card.getName().contains("Silver")) {
                    console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");
                }
                if (card.getName().contains("Gold")) {
                    console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");
                }
                if (card.getName().contains("Platinum")) {
                    console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");
                }
            }

            // Print Kingdom cards
            console.writeln("\nKingdom Cards: ");
            for (DomCard card : kingdomCards) {
                console.writeln(card.getName() + "(" + cardsInSupplyMap.get(card) + "), ");

            }

        }
    }
}