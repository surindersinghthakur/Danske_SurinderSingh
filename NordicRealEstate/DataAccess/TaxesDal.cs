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
using System.Linq;
using System.Transactions;

namespace NordicRealEstate.Api.DataAccess
{
	public class TaxesDal : DbBase, ITaxes
	{
		public TaxesDal(IConfiguration _configuration) : base(_configuration)
		{

		}
		private List<string> Validate(TblTaxes tax)
		{
			List<string> validationMsg = new List<string>();
			if (tax.MunicipalityId <= 0)
				validationMsg.Add("Please enter municipality to apply tax."); //TODO: pick from resource?

			if (tax.DtStart <= DateTime.MinValue || tax.DtEnd <= DateTime.MinValue)
				validationMsg.Add("Start or end date is invalid"); //TODO: pick from resource?


			return validationMsg;
		}

		public void Add(List<TblTaxes> datas)
		{
			//use transaction to avoid any failures for any one.
			using var tran = new TransactionScope();
			foreach (var data in datas)
			{
				Add(data);
			}

			tran.Complete();
		}

		public int Add(TblTaxes entity)
		{
			var validationMsg = Validate(entity);
			if (validationMsg.Any())
				throw new Exception(string.Join(',', validationMsg));

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

		public GenericApiResponse<TblTaxes> GetAll(RequestBase req)
		{
			ORDER_BY = string.IsNullOrEmpty(req.SortBy) ? "TaxType" : $" {req.SortBy} ";
			ORDER_BY += string.IsNullOrEmpty(req.SortDir) ? " ASC" : $" {req.SortDir} ";

			return GetInternal<TblTaxes>(req);
		}

		public GenericApiResponse<TblTaxes> GetById(int id)
		{
			WHERE_CLAUSE = " id = @id";
			ORDER_BY = "TaxType";

			DB_PARAMS = new DynamicParameters();
			DB_PARAMS.Add("@id", id);

			return GetInternal<TblTaxes>(new RequestBase { });
		}

	}
}
