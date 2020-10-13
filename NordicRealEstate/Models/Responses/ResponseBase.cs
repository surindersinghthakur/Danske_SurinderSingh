namespace NordicRealEstate.Api.Models.Responses
{
	public class ResponseBase
	{
		public int? Offset { get; set; }

		public int? TotalCount { get; set; }

		//how many rows fetched
		public int? Count { get; set; }

		public bool Success { get; set; }

		public string Message { get; set; }
	}
}
