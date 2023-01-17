using System;
using System.Threading;
using System.Timers;
namespace Tetris
{
	internal class Program
	{
		const int TIMER_INTERVAL = 500;
		static System.Timers.Timer timer;
		static private Object _lockObject = new object();

		static Figure currentFigure;
		static FigureGenetator generator;
		static void Main(string[] args)
		{
			DrawerProvider.Drawer.InitField();

			generator = new FigureGenetator(Field.Width / 2, 0);

			currentFigure = generator.GetNewFigure();
			SetTimer();

			while(true)
			{
				if(Console.KeyAvailable)
				{
					var key = Console.ReadKey();
					Monitor.Enter(_lockObject);
					var result = HandelKay(currentFigure, key.Key);
					ProcessResult(result, ref currentFigure);
					Monitor.Exit(_lockObject);
				}
			}


		}

		private static void SetTimer()
		{
			// Create a timer with a two second interval.
			timer = new System.Timers.Timer(TIMER_INTERVAL);
			// Hook up the Elapsed event for the timer. 
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		private static void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			Monitor.Enter(_lockObject);
			var result = currentFigure.TryMove(Direction.DOWN);
			ProcessResult(result, ref currentFigure);
			Monitor.Exit(_lockObject);

		}

		private static bool ProcessResult(Result result, ref Figure currentFigure)
		{
			if(result == Result.HEAP_STRIKE || result == Result.DOWN_BORDER_STRIKE)
			{
				Field.AddFigure(currentFigure);
				Field.TryDeleteLines();
				 
				if(currentFigure.IsOnTop())
				{
					DrawerProvider.Drawer.WriteGameOver();
					timer.Elapsed -= OnTimedEvent;
					return true;
				}
				else
				{
					currentFigure = generator.GetNewFigure();
					return false;
				}

			}
			else
			{
				return false;
			}
		}

		private static Result HandelKay(Figure currentFigure, ConsoleKey key)
		{
			switch(key)
			{
				case ConsoleKey.LeftArrow:
					currentFigure.TryMove(Direction.LEFT);
					break;
				case ConsoleKey.RightArrow:
					currentFigure.TryMove(Direction.RIGHT);
					break;
				case ConsoleKey.DownArrow:
					currentFigure.TryMove(Direction.DOWN);
					break;
				case ConsoleKey.Spacebar:
					currentFigure.TryRotate();
					break;
			}
			return Result.SUCCESS;
		}
	}


}
