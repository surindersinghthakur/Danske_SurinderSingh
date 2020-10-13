using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;
using System.Collections.Generic;

namespace NordicRealEstate.Api.Interfaces
{
	public interface ITaxes
	{
		/// <summary>
		/// Add a new tax schedule
		/// </summary>
		/// <param name="entity">Tax schedule to add</param>
		/// <returns></returns>
		int Add(TblTaxes entity);

		/// <summary>
		/// Add new tax schedules
		/// </summary>
		/// <param name="datas">Multiple Tax schedules</param>
		void Add(List<TblTaxes> datas);

		/// <summary>
		/// Get any tax schedule by Id (PK)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		GenericApiResponse<TblTaxes> GetById(int id);

		/// <summary>
		/// Get all tax schedules based on pagination
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		GenericApiResponse<TblTaxes> GetAll(RequestBase req);
	}
}
