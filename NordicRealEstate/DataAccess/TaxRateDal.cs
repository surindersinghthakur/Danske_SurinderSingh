using Dapper;
using Microsoft.Extensions.Configuration;
using NordicRealEstate.Api.Interfaces;
using NordicRealEstate.Api.Models.Responses;
using NordicRealEstate.Api.Models.Transients;
using System;
using System.Data.SqlClient;

namespace NordicRealEstate.Api.DataAccess
{
	public class TaxRateDal : DbBase, ITaxUtil
	{
		public TaxRateDal(IConfiguration _configuration) : base(_configuration)
		{

		}

		public GenericApiResponse<TaxRateModel> GetTaxRate(string municipality, DateTime taxDate)
		{
			if (string.IsNullOrEmpty(municipality))
				throw new Exception("Please enter municipality name to get rate");

			string sql = "EXEC sp_get_tax_applied @municipality, @taxDate";
			using(var conn = new SqlConnection(_connectionString))
			{
				var data = conn.Query<TaxRateModel>(sql, new { municipality, taxDate });
				return new GenericApiResponse<TaxRateModel>
				{
					Data = data,
					Success = true
				};
			}
		}
	}
}
