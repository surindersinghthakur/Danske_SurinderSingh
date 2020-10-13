namespace NordicRealEstate.Api.Models.Requests
{
	public class RequestBase
	{
		public int? Page { get; set; }

		public int? StartRowIndex { get; set; }
		public int? PageSize { get; set; }

		public bool IncludeTotal { get; set; }

		public string SortBy { get; set; }

		public string SortDir { get; set; }

		public string CsvFieldList { get; set; }

		public object SearchFieldValue { get; set; }

		public bool ExactMatch { get; set; }
	}
}
