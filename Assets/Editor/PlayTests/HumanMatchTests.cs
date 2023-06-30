using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HumanMatchTest
{
	[UnityTest]
	public IEnumerator TestWinTopRow()
	{
		//load from the main scene
		SceneManager.LoadScene(0);
		//wait for the main menu to load
		yield return GameTest.TestComands.WaitForObject("OfflineMenuPopup(Clone)");
		//set the players to be human controlled
		yield return GameTest.TestComands.Tap("Player1/HumanImage");
		yield return GameTest.TestComands.Tap("Player2/HumanImage");
		//start the game
		yield return GameTest.TestComands.Tap("OkButton");
		//wait for the board to be loaded
		yield return GameTest.TestComands.WaitUntilObjectNotPresent("OfflineMenuPopup(Clone)");

		//first player places on top-left
		yield return GameTest.TestComands.Tap("Board/Cell");
		//second player places on middle-left
		yield return GameTest.TestComands.Tap("Board/Cell (3)");
		//first player places on top-center
		yield return GameTest.TestComands.Tap("Board/Cell (1)");
		//second player places on middle-center
		yield return GameTest.TestComands.Tap("Board/Cell (4)");
		//first player places on top-right
		yield return GameTest.TestComands.Tap("Board/Cell (2)");

		//first player wins
		yield return GameTest.TestComands.WaitForObject("Won1Text");

		//go to main menu
		yield return GameTest.TestComands.Tap("GameOverPopup(Clone)");
	}
}
