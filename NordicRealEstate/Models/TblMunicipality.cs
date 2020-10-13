using Dapper.Contrib.Extensions;
namespace NordicRealEstate.Api.Models
{
	[Table("TblMunicipality")]
	public class TblMunicipality : BaseModel
	{
		public string Name { get; set; }
	}
}
