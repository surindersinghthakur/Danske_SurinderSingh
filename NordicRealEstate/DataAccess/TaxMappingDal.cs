using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using NordicRealEstate.Api.Interfaces;
using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NordicRealEstate.Api.DataAccess
{
	public class TaxMappingDal : DbBase, ITaxMapping
	{
		public TaxMappingDal(IConfiguration _configuration) : base(_configuration)
		{

		}

		public void Add(List<TblTaxMappings> datas)
		{
			foreach (var data in datas)
			{
				Add(data);
			}
		}

		public int Add(TblTaxMappings entity)
		{
			int id = entity.Id;
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				if (id > 0)
				{
					SetBaseFieldsUpdate(entity);
					conn.Update(entity);
				}
				else
				{
					SetBaseFieldsAdd(entity);
					id = Convert.ToInt32(conn.Insert(entity));
				}
			}

			return id;
		}

		public GenericApiResponse<TblTaxMappings> GetAll(RequestBase req)
		{
			ORDER_BY = string.IsNullOrEmpty(req.SortBy) ? "MunicipalityId" : $" {req.SortBy} ";
			ORDER_BY += string.IsNullOrEmpty(req.SortDir) ? " ASC" : $" {req.SortDir} ";

			return GetInternal<TblTaxMappings>(req);
		}

		public GenericApiResponse<TblTaxMappings> GetById(int id)
		{
			WHERE_CLAUSE = " id = @id";
	
			DB_PARAMS = new DynamicParameters();
			DB_PARAMS.Add("@id", id);

			return GetInternal<TblTaxMappings>(new RequestBase { });
		}

	}
}
