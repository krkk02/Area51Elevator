using System;

namespace Area51Elevator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Random rand = new Random();
			string currFloor = ((Floors)rand.Next(Enum.GetValues(typeof(Floors)).Length)).ToString();
			for (int i = 0; i < 5; i++)
			{
				Thread agentThread = new Thread(() => Agents());
				agentThread.Start();
				agentThread.Join();

				Thread elevatorThread = new Thread(() => Elevator(currFloor, Agent.secLevel));
				elevatorThread.Start();
				elevatorThread.Join();

				Console.WriteLine();
			}
			Console.WriteLine("No more agents in the elevator!");
		}

		public static (string name, string secLevel) Agent;

		public static void Agents()
		{
			Random rand = new Random();
			Array nameArr = Enum.GetValues(typeof(AgentNames));
			Array secLevelArr = Enum.GetValues(typeof(SecurityLevels));

			Agent.name = ((AgentNames)nameArr.GetValue(rand.Next(nameArr.Length))).ToString();
			Agent.secLevel = ((SecurityLevels)secLevelArr.GetValue(rand.Next(secLevelArr.Length))).ToString();

			Console.WriteLine($"Welcome Agent {Agent.name} ({Agent.secLevel}) to Area 51! Please select floor.");
		}

		public static bool Security(string floor, string secLevel)
		{
			string successMsg = "Security verification successful! You can enter the floor.";

			if (floor == "Ground" && secLevel == "Confidential")
			{
				Console.WriteLine(successMsg);
				return true;
			}

			if ((floor == "Ground" || floor == "Secret") && secLevel == "Secret")
			{
				Console.WriteLine(successMsg);
				return true;
			}

			if ((floor == "Ground" || floor == "Secret" || floor == "TopSecret" || floor == "SuperTopSecret") 
				&& secLevel == "TopSecret")
			{
				Console.WriteLine(successMsg);
				return true;
			}

			Console.WriteLine("Security verification failed! Please choose other floor.");
			return false;
		}

		public static void Elevator(string currFloor, string secLevel)
		{
			int currFloorIndex = (int)(Floors)Enum.Parse(typeof(Floors), currFloor);
			string nextFloor = "";

			Array floorArr = Enum.GetValues(typeof(Floors));
			do
			{
				Random rand = new Random();
				nextFloor = ((Floors)floorArr.GetValue(rand.Next(floorArr.Length))).ToString();
				Console.WriteLine($"Selected {nextFloor} floor.");

				int nextFloorIndex = (int)(Floors)Enum.Parse(typeof(Floors), nextFloor);
				for (int i = 0; i < Math.Abs(nextFloorIndex - currFloorIndex); i++)
				{
					Thread.Sleep(1000);
				}

				currFloorIndex = nextFloorIndex;
				Console.WriteLine($"Reached {nextFloor} floor.");
			} while (Security(nextFloor, secLevel) == false);
		}

		public enum SecurityLevels
		{
			Confidential,
			Secret,
			TopSecret
		}

		public enum Floors
		{
			Ground,
			Secret,
			TopSecret,
			SuperTopSecret
		}

		public enum AgentNames
		{
			Chen,
			Mcguire,
			Jones,
			Russo,
			Pena,
			Rivas,
			Daniel,
			Lang,
			Bush,
			Miranda,
			Farrell,
			Carlson,
			Ritter,
			Powers,
			Anderson
		}
	}
}