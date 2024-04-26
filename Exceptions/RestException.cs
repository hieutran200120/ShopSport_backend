using System.Collections;
using System.Net;

namespace shopsport.Exceptions
{
	public class RestException : Exception
	{
		public static string STATUS_CODE = "statusCode";
		private HttpStatusCode _errorCode;
		public RestException(HttpStatusCode errorCode, string message) : base(message)
		{
			_errorCode = errorCode;
		}

		public override IDictionary Data => new Dictionary<string, int>()
		{
			{STATUS_CODE, (int)_errorCode }
		};
	}
}
