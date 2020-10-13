using Dapper.Contrib.Extensions;
using System;
using static NordicRealEstate.Api.Extras.GlobalEnums;

namespace NordicRealEstate.Api.Models
{
	[Table("TblTaxes")]
	public class TblTaxes : BaseModel
	{ 
		public int MunicipalityId { get; set; }

		public TaxType TaxType { get; set; }

		public DateTime DtStart { get; set; }

		public DateTime DtEnd { get; set; }

		public decimal Rate { get; set; }
	}
}
