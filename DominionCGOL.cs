
namespace FridayPartyCGOL
{
    public class Dominion{
        public static void main(string[] args){

            console.writeln("Welcome to dominion");
            string input = console.prompt("enter number of players");
            int numberOfPlayers = input.toInt();
            for (int i = 1; i <= numberOfPlayers; i++) {            
                string playerName = console.prompt("enter a name for player"+ i);
                Players.add(new Player(playerName));
            }

            // setup the board:
            ArrayList<DomCard> myKingdomCards = new ArrayList<>(); // should be randomized
            myKingdomCards.add(new Smithy());
            myKingdomCards.add(new Artisan());
            myKingdomCards.add(new Village());
            myKingdomCards.add(new Vassal());
            myKingdomCards.add(new Market());

            Supply theSupply = new Supply(myKingdomCards, numberOfPlayers, true);

            Board gameboard = new Board(Players, theSupply);
            globalMethods.SetactiveBoard(gameboard);
            globalMethods.isGameOver = false;
            ////////////////////////////
            // run the game
            //////////////////////////////////
            
            while (!globalMethods.isGameOver) {
                // get the current player
                Player currentPlayer = gameboard.getActivePlayer();
                currentPlayer.begginningTurn();

                globalMethods.handleActionPhase(currentPlayer);// prints everything

                // after actions: treasures
                while (currentPlayer.getTreasureFromStack(currentPlayer.getHand()).size() != 0) {
                    gameboard.printBoard();
                    gameboard.getActivePlayer().prettyPrintAll();
                    DomCard TreasureToPlay = currentPlayer.promptPlayTreasure();// choose an actioncard to play
                    if (TreasureToPlay == null) {
                        break;
                    }
                    console.writeln("You played: " + TreasureToPlay.getName());
                    currentPlayer.playCard(TreasureToPlay);// execute the treasure
                }
                // after treasuers: its a me: debuy phase
                while (currentPlayer.getNumBuys() != 0) {
                    gameboard.printBoard();
                    gameboard.getActivePlayer().prettyPrintAll();
                    DomCard buyThis = gameboard.promptToBuyFromSupply(0, currentPlayer.getNumCoins(), false);
                    if (buyThis == null) {
                        break;
                    }
                    currentPlayer.addBuys(-1);
                    currentPlayer.gainCard(buyThis);
                    currentPlayer.addCoins(-buyThis.getCost());
                }
                console.writeln("Turn finnished \nyou discard all cards from play and your hand, and draw 5 new cards");

                gameboard.finnishTurn();
                
                // check if the game is over
                if (gameOver(gameboard)) {
                    console.writeln("The game is over!");
                    break;
                }
                gameboard.nextPlayer();
                currentPlayer = gameboard.getActivePlayer();

            }

            // display the final scores
            // displayScores();

        }    
    }
}
