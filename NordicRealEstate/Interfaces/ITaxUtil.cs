using NordicRealEstate.Api.Models.Responses;
using NordicRealEstate.Api.Models.Transients;
using System;

namespace NordicRealEstate.Api.Interfaces
{
	public interface ITaxUtil
	{
		/// <summary>
		/// Get tax rate for particular municipality for a specific date
		/// Checks if Daily rate is available, if not then Weekly, if not then Monthly and last Yearly
		/// here is order of rate pick: Daily, Weekly, Monthly, Yearly
		/// </summary>
		/// <param name="municipality">Name of municipality</param>
		/// <param name="taxDate">Required date to fetch tax rate</param>
		/// <returns>Tax rate</returns>
		GenericApiResponse<TaxRateModel> GetTaxRate(string municipality, DateTime taxDate);
	}
}
