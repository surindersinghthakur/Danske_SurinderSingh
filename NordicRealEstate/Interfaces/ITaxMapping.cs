using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;
using System.Collections.Generic;

namespace NordicRealEstate.Api.Interfaces
{
	public interface ITaxMapping
	{
		int Add(TblTaxMappings entity);

		void Add(List<TblTaxMappings> datas);

		GenericApiResponse<TblTaxMappings> GetById(int id);

		GenericApiResponse<TblTaxMappings> GetAll(RequestBase req);
	}
}
