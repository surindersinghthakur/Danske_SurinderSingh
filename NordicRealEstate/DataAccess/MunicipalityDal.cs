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
	public class MunicipalityDal : DbBase, IMunicipalityDal
	{
		public MunicipalityDal(IConfiguration _configuration) : base(_configuration)
		{

		}

		private List<string> Validate(TblMunicipality entity)
		{
			List<string> validationMsg = new List<string>();
			if (string.IsNullOrEmpty(entity.Name))
				validationMsg.Add("Please enter municipality name to add."); //TODO: pick from resource?
			
			return validationMsg;
		}

		public void Add(List<TblMunicipality> datas)
		{
			//use transaction to avoid any failures for any one.
			using var tran = new TransactionScope();
			foreach (var data in datas)
			{
				Add(data);
			}
			tran.Complete();
		}

		public int Add(TblMunicipality entity)
		{
			var validationMsg = Validate(entity);
			if (validationMsg.Any())
				throw new Exception(string.Join(',', validationMsg));

			int id = entity.Id;
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				if (Exists(entity.Name, conn, entity.Id))
					throw new Exception("Can't add duplicate name!");

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

		private bool Exists(string name, SqlConnection conn, int id = 0)
		{
			var result = conn.ExecuteScalar<int>("SELECT id FROM TblMunicipality WHERE Name = @name AND id <> @id", new { name, id });

			return result > 0;
		}

		public GenericApiResponse<TblMunicipality> GetAll(RequestBase req)
		{
			ORDER_BY = string.IsNullOrEmpty(req.SortBy) ? "Name" : $" {req.SortBy} ";
			ORDER_BY += string.IsNullOrEmpty(req.SortDir) ? " ASC" : $" {req.SortDir} ";

			return GetInternal<TblMunicipality>(req);
		}

		public GenericApiResponse<TblMunicipality> GetById(int id)
		{
			WHERE_CLAUSE = " id = @id";
			ORDER_BY = "Name";

			DB_PARAMS = new DynamicParameters();
			DB_PARAMS.Add("@id", id);

			return GetInternal<TblMunicipality>(new RequestBase { });
		}

	}
}
