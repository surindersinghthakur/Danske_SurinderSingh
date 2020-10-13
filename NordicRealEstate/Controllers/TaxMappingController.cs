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
	public class TaxMappingController : ControllerBase
    {
        readonly ITaxMapping db;

        public TaxMappingController(ITaxMapping _db)
        {
            db = _db;
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<GenericApiResponse<TblTaxMappings>> AddOrUpdate(TblTaxMappings entity)
        {
            try
            {
                var data = db.Add(entity);

                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(new GenericApiResponse<TblTaxMappings> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("add/list")]
        public ActionResult<GenericApiResponse<TblTaxMappings>> AddOrUpdate(List<TblTaxMappings> entities)
        {
            try
            {
                db.Add(entities);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(new GenericApiResponse<TblTaxMappings> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public ActionResult<GenericApiResponse<TblTaxMappings>> List(int id)
        {
            try
            {
                var data = db.GetById(id);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(new GenericApiResponse<TblTaxMappings> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("get/all")]
        public ActionResult<GenericApiResponse<TblTaxMappings>> List([FromBody] RequestBase req)
        {
            try
            {
                var data = db.GetAll(req);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(new GenericApiResponse<TblTaxMappings> { Success = false, Message = ex.Message });
            }
        }
    }
}
