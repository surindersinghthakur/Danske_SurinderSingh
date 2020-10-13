using System.Collections.Generic;

namespace NordicRealEstate.Api.Models.Responses
{
	public class GenericApiResponse<T> : ResponseBase
	{
		public IEnumerable<T> Data { get; set; }
	}
}
