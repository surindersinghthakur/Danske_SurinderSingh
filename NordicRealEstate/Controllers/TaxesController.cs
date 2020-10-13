using System;
using System.Collections.Generic;
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
	public class TaxesController : ControllerBase
	{
        readonly ITaxes db;

        public TaxesController(ITaxes _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("add/list")]
        public ActionResult<GenericApiResponse<string>> AddOrUpdate([FromBody] List<TblTaxes> entities)
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

        [HttpPost]
        [Route("add")]
        public ActionResult<GenericApiResponse<TblTaxes>> AddOrUpdate([FromBody] TblTaxes entity)
        {
            try
            {
                var data = db.Add(entity);

                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public ActionResult<GenericApiResponse<TblTaxes>> List(int id)
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
        public ActionResult<GenericApiResponse<TblTaxes>> List([FromBody] RequestBase req)
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
