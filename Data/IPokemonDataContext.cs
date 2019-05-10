using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using Npgsql;

using babelhealth.Models;

namespace babelhealth.Data
{
  /// <summary>
  /// Interface for Pokemon data operations
  /// </summary>
  public interface IPokemonDataContext : IDisposable
  {
    /// <summary>
    /// Get a Pokemon by its ID
    /// </summary>
    /// <param name="id">Pokemon ID, or National Number</param>
    Pokemon GetById(int id);

    /// <summary>
    /// Get the list of Pokemon
    /// </summary>
    /// <returns></returns>
    List<Pokemon> GetList();
    
    /// <summary>
    /// Create a new Pokemon
    /// </summary>
    Pokemon Create(Pokemon pokemon);

    /// <summary>
    ///  Update a Pokemon
    /// </summary>
    Pokemon Update(Pokemon pokemon);

    /// <summary>
    /// Delete a Pokemon
    /// </summary>
    /// <param name="id">Pokemon's ID, or National Number</param>
    void Delete(int id);
  }
}
