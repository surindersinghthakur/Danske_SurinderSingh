using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NordicRealEstate.Api.Interfaces;
using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Requests;
using NordicRealEstate.Api.Models.Responses;

namespace NordicRealEstate.Api.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class MunicipalityController : ControllerBase
	{
		readonly IMunicipalityDal db;

		public MunicipalityController(IMunicipalityDal _db)
		{
			db = _db;
		}

        [HttpPost]
        [Route("add")]
        public ActionResult<GenericApiResponse<TblMunicipality>> AddOrUpdate(TblMunicipality entity)
        {
            try
            {
                var data = db.Add(entity);
                entity.Id = data;

                return Ok(new GenericApiResponse<TblMunicipality> { 
                    Success = true,
                    Count = data
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add/list")]
        public ActionResult<GenericApiResponse<TblMunicipality>> AddOrUpdate(List<TblMunicipality> entities)
        {
            try
            {
                db.Add(entities);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public ActionResult<GenericApiResponse<TblMunicipality>> List(int id)
        {
            try
            {
                var data = db.GetById(id);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/all")]
        public ActionResult<GenericApiResponse<TblMunicipality>> List([FromBody] RequestBase req)
        {
            try
            {
                var data = db.GetAll(req);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
