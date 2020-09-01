using System;

namespace ts
{
	public class TakeDataFromUser
	{
		private int id = 0;
		public TakeDataFromUser(int id)
        {
			this.id = id;
        }

		public int getId()
        {
			return this.id;
        }
	}

}