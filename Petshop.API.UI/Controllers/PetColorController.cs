using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Enteties;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Petshop.RestAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetColorController : ControllerBase
    {
        IPetColorService _petColorService;

        public PetColorController(IPetColorService petColorService)
        {
            _petColorService = petColorService;
        }
        // GET: api/<PetColorController>
        [HttpGet]
        public ActionResult<IEnumerable<PetColor>> Get([FromQuery] FilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                {
                    if (string.IsNullOrEmpty(filter.SortOrder))
                    {
                        try
                        {
                            return Ok(_petColorService.GetAllPetColors());
                        }
                        catch (Exception e)
                        {
                            return NotFound(e.Message);
                        }
                    }
                    else if (filter.SortOrder.ToLower().Equals("asc") || filter.SortOrder.ToLower().Equals("desc"))
                    {
                        try
                        {
                            return Ok(_petColorService.GetAllFilteredPetColors(filter));
                        }
                        catch (Exception e)
                        {
                            return NotFound(e.Message);
                        }
                    }
                    else
                    {
                        return BadRequest("You need to enter SortOrder 'asc' or 'desc'");
                    }
                }
                else
                {
                    try
                    {
                        return Ok(_petColorService.GetAllFilteredPetColors(filter));
                    }
                    catch (Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return BadRequest("You need to enter both a SearchTerm and a SearchValue");
                }
                else
                {
                    try
                    {
                        List<PetColor> allColorsFound = _petColorService.SearchPetColor(filter);
                        if (allColorsFound.Count < 1)
                        {
                            return NotFound("No pets with those parameters could be found.");
                        }
                        else
                        {
                            return Ok(allColorsFound);
                        }

                    }
                    catch (Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }

            }

        }

        // GET api/<PetColorController>/5
        [HttpGet("{id}")]
        public ActionResult<PetColor> Get(int id)
        {
            try
            {
                PetColor theColor = _petColorService.FindPetColorByIdWithPets(id);
                if (theColor == null)
                {
                    return NotFound("I am sorry could not find a type with that Id.");
                }
                else
                {
                    return Ok(theColor);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<PetColorController>
        [HttpPost]
        public ActionResult<PetColor> Post([FromBody] PetColor theNewColor)
        {
            if (string.IsNullOrEmpty(theNewColor.PetColorName))
            {
                return BadRequest("You need to enter a name to create a new Color.");
            }
            else
            {
                try
                {
                    return Created("Successfully created the following petColor", _petColorService.AddNewPetColor(theNewColor));
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
        }

        // PUT api/<PetColorController>/5
        [HttpPut("{id}")]
        public ActionResult<PetColor> Put(int id, [FromBody] PetColor theUpdatedPetColor)
        {
            if (id != theUpdatedPetColor.PetColorId || id == 0)
            {
                return BadRequest("The Id's must match, and may not be 0.");
            }
            else if (string.IsNullOrEmpty(theUpdatedPetColor.PetColorName))
            {
                return BadRequest("You need to enter a name for the updated color.");
            }
            else
            {
                try
                {
                    return Accepted(_petColorService.UpdatePetColor(theUpdatedPetColor));
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
        }

        // DELETE api/<PetColorController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _petColorService.DeletePetColor(id);
                return Accepted($"Successfully deleted petColor with the id {id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
