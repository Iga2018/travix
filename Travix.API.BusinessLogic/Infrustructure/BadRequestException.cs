using System;

namespace Travix.API.BusinessLogic.Infrustructure
{
	public class OperationException : Exception
	{
		public OperationException(string message) : base(message)
		{

		}
	}
}
