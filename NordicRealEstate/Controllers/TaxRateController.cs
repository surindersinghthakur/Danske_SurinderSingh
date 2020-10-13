using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NordicRealEstate.Api.Interfaces;
using NordicRealEstate.Api.Models;
using NordicRealEstate.Api.Models.Responses;
using NordicRealEstate.Api.Models.Transients;

namespace NordicRealEstate.Api.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TaxRateController : ControllerBase
	{
		readonly ITaxUtil db;

		public TaxRateController(ITaxUtil _db)
		{
			db = _db;
		}

        [HttpGet]
        [Route("get")]
        public ActionResult<GenericApiResponse<TaxRateModel>> List(string municipality, DateTime taxDate)
        {
            try
            {
                var data = db.GetTaxRate(municipality, taxDate);

                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
