using Dapper;
using Microsoft.Extensions.Configuration;
using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace NordicRealEstate.Api.DataAccess
{
	public class DbBase
	{
		private int AuthUserId;

		public IConfiguration _config;
		public string _connectionString;

		public string SELECT_SQL { get; set; }

		public string WHERE_CLAUSE { get; set; }

		public string ORDER_BY { get; set; }

		public DynamicParameters DB_PARAMS { get; set; }

		public DbBase(IConfiguration _configuration)
		{
			_config = _configuration;
			_connectionString = _config["ConnectionString"];
			SetAuthUserId();
		}

		/// <summary>
		/// TODO: set auth id from the user authenticated using claims and identity
		/// </summary>
		public void SetAuthUserId()
		{
			AuthUserId = 1;
		}


		protected void SetBaseFieldsAdd<T>(T model)
		{
			var entity = model as BaseModel;
			entity.DtCreated = DateTime.Now;
			entity.CreatedUserId = AuthUserId;			

			if (HasProperty(entity, "IsActive"))
				entity.IsActive = true;

			if (HasProperty(entity, "IsDeleted"))
			{
				entity.IsDeleted = false;
			}
		}

		protected void SetBaseFieldsUpdate<T>(T model)
		{
			var entity = model as BaseModel;
			entity.UpdatedUserId = AuthUserId;
			entity.DtUpdated = DateTime.Now;

			if (HasProperty(entity, "IsActive"))
				entity.IsActive = true;

			if (HasProperty(entity, "IsDeleted") && !entity.IsDeleted)
			{
				entity.IsDeleted = false;				
			}

		}

		protected void SetBaseFieldsUpdate<T, T1>(T oldModel, T1 newModel)
		{
			var oldEntity = oldModel as BaseModel;
			var newEntity = newModel as BaseModel;

			SetBaseFieldsUpdate(newEntity);

			newEntity.DtCreated = oldEntity.DtCreated;
			newEntity.CreatedUserId = oldEntity.CreatedUserId;

		}

		protected void SetBaseFieldsDelete<T>(T model)
		{
			var entity = model as BaseModel;
			entity.DeletedUserId = AuthUserId;
			entity.DtDeleted = DateTime.Now;
			entity.IsDeleted = true;
		}

		protected bool HasProperty<T>(T type, string prop)
		{
			var property = type.GetType().GetProperty(prop);

			return property != null;
		}

		internal virtual GenericApiResponse<T> GetInternal<T>(RequestBase req)
		{
			var tblName = typeof(T).Name;
			var fieldList = string.IsNullOrEmpty(req.CsvFieldList) ? "*" : req.CsvFieldList;
			SELECT_SQL = $"SELECT {fieldList} FROM {tblName}";

			if (!string.IsNullOrWhiteSpace(WHERE_CLAUSE))
				WHERE_CLAUSE = $" WHERE {WHERE_CLAUSE}";

			if (!string.IsNullOrWhiteSpace(ORDER_BY))
				ORDER_BY = $" ORDER BY {ORDER_BY} ";

			if (DB_PARAMS == null)
				DB_PARAMS = new DynamicParameters();

			string sql = $"{SELECT_SQL} {WHERE_CLAUSE} {ORDER_BY}";

			int? offset = 0;

			if (req.PageSize != null && req.Page > 0 || req.StartRowIndex != null)
			{
				if (req.StartRowIndex != null)
					offset = req.StartRowIndex;
				else
					offset = (req.Page - 1) * req.PageSize;

				sql += @" OFFSET @Offset ROWS
							FETCH NEXT @PageSize ROWS ONLY;";

				DB_PARAMS.Add("Offset", offset);
				DB_PARAMS.Add("PageSize", req.PageSize);
			}
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				GenericApiResponse<T> response = new GenericApiResponse<T>();
				if (req.IncludeTotal)
				{					
					sql += $@"; SELECT COUNT(*) FROM {tblName} {WHERE_CLAUSE} ";
					using (var multi = conn.QueryMultiple(sql, DB_PARAMS))
					{
						response.Data = multi.Read<T>().ToList();
						response.TotalCount = multi.ReadFirst<int>();
						response.Offset = offset;
						response.Success = true;
					}
				}
				else
				{
					response.Data = conn.Query<T>(sql, DB_PARAMS).AsEnumerable();
					response.TotalCount = response.Data == null ? 0 : response.Data.Count();
					response.Success = true;
				}

				response.Count = response.Data == null ? 0 : response.Data.Count();

				return response;
			}
		}
	}
}
