using Newtonsoft.Json;
using NordicRealEstate.Api.DataAccess;
using NordicRealEstate.Api.Interfaces;
using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Responses;
using RestSharp;
using System;
using System.Net;
using System.Runtime.Serialization;
using Xunit;

namespace Tests
{
	public class IntegrationTests
	{
		private const string BASE_API_PATH = "https://localhost:44367/api";

		[Theory]
		[InlineData("Copenhagen")]
		public void CreateMunicipality(string name)
		{
			TblMunicipality entity = new TblMunicipality
			{
				Name = name
			};

			string url = $"{BASE_API_PATH}/municipality/add";
			var response = InvokeApi(url, entity, Method.POST);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var data = JsonConvert.DeserializeObject<GenericApiResponse<TblMunicipality>>(response.Content);
			Assert.True(data.Success);
		}

		public IRestResponse InvokeApi(string url, object bodyData, Method method = Method.GET)
		{
			IRestClient client = new RestClient(url);
			IRestRequest request = new RestRequest(method);
			//request.AddHeader("Authorization", string.Format("Bearer {0}", token.AccessToken));
			if (bodyData != null)
			{
				request.AddJsonBody(bodyData);
			}

			IRestResponse response = client.Execute(request);
			return response;
		}

	}
}
