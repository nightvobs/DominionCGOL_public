
namespace FridayPartyCGOL
{
    public class globalMethods {
        public static Board activeBoard = null;
        public static Boolean isGameOver = false;

        public static string inputControl(int max, boolean isNotOptional) {
            string input = System.console().readLine();
            int choice = -1;
            while (choice < 1 || choice > max) {
                try {
                    choice = Integer.parseInt(input); // convert input to a card selection
                    // get the selected card from the map using its index (choice)
                    return input;
                } catch (NumberFormatException e) {
                    if (input.equals("n")) { // player chose skip "n"
                        if (isNotOptional) {
                            console.writeln("sorry, this is not an optional selection, you MUST choose an option");
                        } else {
                            return null; // players chose the skip, and input is accepted
                        }
                    } else {
                        console.writeln("Invalid choice, please enter a number between 1 and " + max);
                        return inputControl(max, isNotOptional);
                    }
                }
            }
            return input;
        }

        public static void SetactiveBoard(Board gameBoard) {
            activeBoard = gameBoard;

        }

        public static DomCard chooseCard_Selection(string message, Map<DomCard, Integer> Avail_Selection,
                boolean isNotOptional) {
            DomCard selectedCard = null;
            string options = "";
            int i = 1; // start index from 1
            console.writeln("The Available selection: ");
            for (DomCard card : Avail_Selection.keySet()) {
                options += i + ") |" + card.getName() + "|  available: " + Avail_Selection.get(card) + "\n";
                i++;
            }

            console.writeln(message);
            console.writeln(options);
            if (!isNotOptional) {
                console.writeln("to select no cards, type n");
            }

            string input = globalMethods.inputControl(Avail_Selection.size(), isNotOptional);
            if (input == null) { // player chose skip "n"
                return null;
            }
            int choice = Integer.parseInt(input);
            selectedCard = (DomCard) Avail_Selection.keySet().toArray()[choice - 1];

            return selectedCard;
        }

        public static void handleActionPhase(Player currentPlayer) {
            while (currentPlayer.getNumActions() != 0 || currentPlayer.getNumVillagers() != 0) {
                activeBoard.printBoard();
                currentPlayer.prettyPrintAll();
                // ask the player what action they want to take
                DomCard ActioncardToPlay = currentPlayer.promptPlayAction();// choose an actioncard to play
                if (ActioncardToPlay == null) {
                    break;
                }
                // pay the activation cost and execute the action
                if (currentPlayer.getNumActions() != 0) {
                    currentPlayer.addActions(-1);
                } else {
                    currentPlayer.addVillagers(-1);
                }
                console.writeln("You played: " + ActioncardToPlay.getName());
                console.writeln(":  " + ActioncardToPlay.getEffect());
                currentPlayer.playCard(ActioncardToPlay);

            }
        }

    }
}