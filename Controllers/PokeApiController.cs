using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using babelhealth.Data;
using babelhealth.Models;

namespace babelhealth.Controllers
{
    /// <summary>
    /// Controller for Pokemon CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PokeApiController : BaseController
    {
        private IPokemonDataContext _dataContext;

        /// <summary>
        /// Constructor with DI parameters
        /// </summary>
        /// <param name="loggerFactory">DI logger factory</param>
        /// <param name="dataContext">DI data context</param>
        public PokeApiController(ILoggerFactory loggerFactory, IPokemonDataContext dataContext) : base(loggerFactory)
        {
            _dataContext = dataContext;
            Log("************ Configuration: " + DataContextHelper.ConnectionStrings["PokemonDatabase"]);
        }

        /// <summary>
        /// Returns a list of all Pokemon
        /// </summary>
        [HttpGet("Get")]
        public ActionResult<ResponseModel<List<Pokemon>>> Get()
        {
            // return result;
            Log("About to query for all pokemon ---->");
            using (_dataContext)
            {
                var pokemon = _dataContext.GetList();
                Log(string.Format("----> {0} total pokemon retrieved", pokemon.Count));
                return new ResponseModel<List<Pokemon>>
                {
                    Status = StatusType.Success,
                    Data = pokemon
                };
            }
        }

        /// <summary>
        /// Returns the details of a specific Pokemon
        /// </summary>
        /// <param name="id">The unique identifier for the Pokemon</param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public ActionResult<ResponseModel<Pokemon>> Get(int id)
        {
            Log(string.Format("About to query for a pokemon {0} ---->", id));
            using (_dataContext)
            {
                var pokemon = _dataContext.GetById(id);
                string logMessage;
                var response = new ResponseModel<Pokemon>();
                if (pokemon != null)
                {
                    logMessage = string.Format("----> pokemon {0}, {1} retrieved", id, pokemon.Name);
                    response.Status = StatusType.Success;
                    response.Data = pokemon;
                }
                else
                {
                    logMessage = string.Format("----> pokemon {0} not found!!", id);
                    response.Status = StatusType.Failure;
                    response.Message = "Pokemon not found";
                }

                Log(logMessage);
                return response;
            }
        }

        /// <summary>
        /// Creates a new Pokemon
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        [HttpPut("Put")]
        public ActionResult<ResponseModel<Pokemon>> Put(Pokemon pokemon)
        {
            Log(string.Format("About to create pokemon {0}, {1} ---->", pokemon.Id, pokemon.Name));
            var responseModel = new ResponseModel<Pokemon>();
            try
            {
                using (_dataContext)
                {
                    pokemon = _dataContext.Create(pokemon);
                    Log(string.Format("----> pokemon {0}, {1} created", pokemon.Id, pokemon.Name));
                    responseModel.Status = StatusType.Success;
                    responseModel.Data = pokemon;
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("Creating  pokemon {0}, {1} failed: {2}, {3}", pokemon.Id, pokemon.Name, ex.Message, ex.StackTrace);
                Log(error);
                responseModel.Status = StatusType.Failure;
                responseModel.Message = error;
            }
            return responseModel;
        }

        /// <summary>
        /// Updates a Pokemon
        /// </summary>
        [HttpPost("Post")]
        public ActionResult<ResponseModel<Pokemon>> Post(Pokemon pokemon)
        {
            var response = new ResponseModel<Pokemon>();
            try
            {
                using (_dataContext)
                {
                    Log(string.Format("About to update pokemon {0}, {1} ---->", pokemon.Id, pokemon.Name));
                    _dataContext.Update(pokemon);
                    Log(string.Format("----> pokemon {0}, {1} updated", pokemon.Id, pokemon.Name));

                    response.Status = StatusType.Success;
                    response.Data = pokemon;
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("Updating  pokemon {0}, {1} failed: {2}, {3}", pokemon.Id, pokemon.Name, ex.Message, ex.StackTrace);
                Log(error);
                response.Status = StatusType.Failure;
                response.Message = error;
            }

            return response;
        }

        /// <summary>
        /// Deletes a Pokemon
        /// </summary>
        /// <param name="id">ID, or National Nubmer, of the Pokemon</param>
        [HttpDelete("Delete/{id}")]
        public ActionResult<ResponseModel<List<Pokemon>>> Delete(int id)
        {
            var response = new ResponseModel<List<Pokemon>>();
            try
            {
                if (id == 800) // artificial error to test error handling
                {
                    response.Status = StatusType.Failure;
                    response.Message = "You are forbidden from deleting this Pokemon. How dare you!";
                }
                else
                {
                    using (_dataContext)
                    {
                        Log(string.Format("About to delete pokemon {0} ---->", id));
                        _dataContext.Delete(id);
                        Log(string.Format("----> pokemon {0} deleted", id));

                        response.Data = _dataContext.GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("Deleting  pokemon {0} failed: {1}, {2}", id, ex.Message, ex.StackTrace);
                Log(error);
                response.Status = StatusType.Failure;
                response.Message = error;
            }
            return response;
        }
    }
}