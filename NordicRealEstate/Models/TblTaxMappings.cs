using Dapper.Contrib.Extensions;
using System;

namespace NordicRealEstate.Api.Models
{
	[Table("TblTaxMappings")]
	public class TblTaxMappings : BaseModel
	{
		public int MunicipalityId { get; set; }

		public int TaxId { get; set; }

		public DateTime EffectiveDate { get; set; }
	}
}
