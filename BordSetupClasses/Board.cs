
namespace FridayPartyCGOL
{
    public class Board {
        private ArrayList<Player> players;
        private Supply the_Supply;
        private CardCollection trashPile;
        private Player actiPlayer = null;

        public Board(ArrayList<Player> players, Supply startingSupply) {
            this.players = players;
            this.the_Supply = startingSupply;
            this.trashPile = new CardCollection(new ArrayList<DomCard>());
            this.actiPlayer = players.get(0);
        }

        public Player getActivePlayer() {
            return this.actiPlayer;
        }

        public void nextPlayer() {
            int index = players.indexOf(actiPlayer);
            if (index == players.size() - 1) {
                actiPlayer = players.get(0);
            } else {
                actiPlayer = players.get(index + 1);
            }
            console.writeln("New turn for Player" + actiPlayer.getName());
        }

        public int getNumCards_supply(DomCard card) {
            return the_Supply.getNumCards(card);
        }

        // creates a new hashmap of the affordable cards, from all cards in the supply
        public HashMap<DomCard, Integer> getCards_CostingBetween(int minCost, int maxCost) {
            HashMap<DomCard, Integer> affordableCards = new HashMap<>();
            for (DomCard card : this.the_Supply.getCardMap().keySet()) {
                if (card.getCost() <= maxCost) {
                    if (this.the_Supply.getNumCards(card) > 0) {
                        affordableCards.put(card, this.the_Supply.getNumCards(card));
                    }
                }
            }
            return affordableCards;
        }

        // creates a new hashmap of the affordable cards, from all NON-Victorycards in
        // the supply
        public Map<DomCard, Integer> getNonVP_CostingBetween(int minCost, int maxCost) {
            Map<DomCard, Integer> affordableCards = new HashMap<>();
            for (DomCard card : this.the_Supply.getCardMap().keySet()) {
                if (card.getCost() <= maxCost && card.getCost() < maxCost) {
                    if (this.the_Supply.getNumCards(card) > 0) {
                        if (card.getType() != "Victory")
                            affordableCards.put(card, this.the_Supply.getNumCards(card));
                    }

                }
            }
            return affordableCards;
        }

        public DomCard promptToGainFromSupply(int minCost, int maxCost, boolean isNotOptional) {
            // get a list of all cards in the supply with a cost within the specified range
            Map<DomCard, Integer> affordableCards = this.getCards_CostingBetween(minCost, maxCost);

            // prompt the player to choose a card to gain
            DomCard chosenCard = globalMethods.chooseCard_Selection("Choose a card to gain", affordableCards,
                    isNotOptional);

            // if the player chose a valid card, remove it from the supply and return it
            if (chosenCard != null) {
                this.the_Supply.removeCardsFromSupply(chosenCard, 1);
                return chosenCard;
            }

            // if the player did not choose a valid card, return null
            return null;
        }

        public DomCard promptToBuyFromSupply(int minCost, int maxCost, boolean isNotOptional) {
            // get a list of all cards in the supply with a cost within the specified range
            Map<DomCard, Integer> affordableCards = this.getCards_CostingBetween(minCost, maxCost);

            // prompt the player to choose a card to gain
            DomCard chosenCard = globalMethods.chooseCard_Selection("Choose a card to buy", affordableCards,
                    isNotOptional);

            // if the player chose a valid card, remove it from the supply and return it
            if (chosenCard != null) {
                this.the_Supply.removeCardsFromSupply(chosenCard, 1);
                return chosenCard;
            }

            // if the player did not choose a valid card, return null
            return null;
        }

        public void printBoard() {
            printSupply();
            console.writeln("Trash Pile: " + this.trashPile.size() + " cards");
        }

        public void printSupply() {
            console.writeln("The Supply:");
            this.the_Supply.prettyPrint();
        }

        public void finnishTurn() {
            actiPlayer.cleanUp();
            actiPlayer.drawXcardsFrom_Deck(5);
            actiPlayer.prettyPrintAll();
            console.writeln("next players turn!");
        }

        // Other methods for managing game state
    }
}