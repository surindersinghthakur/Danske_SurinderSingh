using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;
using System.Collections.Generic;

namespace NordicRealEstate.Api.Interfaces
{
	public interface IMunicipalityDal
	{
		/// <summary>
		/// Add a new municipality, updates if id passed is > 0
		/// </summary>
		/// <param name="entity">entity to insert</param>
		/// <returns></returns>
		int Add(TblMunicipality entity);

		/// <summary>
		/// Adds multiple municipalities, can be used in import data
		/// </summary>
		/// <param name="datas">Mutiple entities</param>
		void Add(List<TblMunicipality> datas);


		/// <summary>
		/// Get by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		GenericApiResponse<TblMunicipality> GetById(int id);


		/// <summary>
		/// Get all data from municipality based on pagination
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		GenericApiResponse<TblMunicipality> GetAll(RequestBase req);
	}
}
