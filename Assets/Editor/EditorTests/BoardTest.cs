using NUnit.Framework;
using UnityEditor;

[TestFixture]
public class BoardTest
{
	// TopLeft    - TopCenter    - TopRight
	// MidLeft    - MidCenter    - MidRight
	// BottomLeft - BottomCenter - BottomRight
	private enum EBoardPosition
	{
		TopLeft      = 0,
		TopCenter    = 1,
		TopRight     = 2,
		MidLeft      = 3,
		MidCenter    = 4,
		MidRight     = 5,
		BottomLeft   = 6,
		BottomCenter = 7,
		BottomRight  = 8
	}

	private bool Move(Board b, EBoardPosition p)
	{
		return b.PlaceMove((int)p);
	}

	[SetUp]
	public void Init() { }

	[Test]
	public void TestDraw()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.TopCenter);

		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.MidCenter);

		Move(board, EBoardPosition.TopRight);
		Move(board, EBoardPosition.BottomLeft);

		Move(board, EBoardPosition.BottomCenter);
		Move(board, EBoardPosition.MidRight);

		Move(board, EBoardPosition.BottomRight);

		Assert.IsTrue(board.IsGameOver && !board.Winner.HasValue);
	}

	[Test]
	public void TestWinTopRow()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.TopCenter);
		Move(board, EBoardPosition.MidCenter);
		Move(board, EBoardPosition.TopRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinMidRow()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.MidCenter);
		Move(board, EBoardPosition.TopCenter);
		Move(board, EBoardPosition.MidRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinBottomRow()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.BottomLeft);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.BottomCenter);
		Move(board, EBoardPosition.TopCenter);
		Move(board, EBoardPosition.BottomRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinLeftColumn()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.BottomLeft);
		Move(board, EBoardPosition.TopRight);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.TopCenter);
		Move(board, EBoardPosition.TopLeft);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinCenterColumn()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.TopCenter);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.MidCenter);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.BottomCenter);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinRightColumn()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.TopRight);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.MidRight);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.BottomRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinTopLeftToBottomRightDiagonal()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.TopLeft);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.MidCenter);
		Move(board, EBoardPosition.MidRight);
		Move(board, EBoardPosition.BottomRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}

	[Test]
	public void TestWinBottomLeftToTopRightDiagonal()
	{
		Board board = new Board(3);
		Move(board, EBoardPosition.BottomLeft);
		Move(board, EBoardPosition.MidLeft);
		Move(board, EBoardPosition.MidCenter);
		Move(board, EBoardPosition.MidRight);
		Move(board, EBoardPosition.TopRight);
		Assert.IsTrue(board.Winner.HasValue && board.Winner.Value == 0);
	}
}
