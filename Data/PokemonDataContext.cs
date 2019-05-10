using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Npgsql;

using babelhealth.Models;

namespace babelhealth.Data
{
  /// <summary>
  /// Pokemon data context for Redshift
  /// </summary>
  public class PokemonDataContext : DbContext, IPokemonDataContext
  {
    private DbSet<Pokemon> Pokemon { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(DataContextHelper.ConnectionStrings["PokemonDatabase"]);
    }

    public Pokemon GetById(int id)
    {
      return Pokemon.Where(p => p.Id == id).SingleOrDefault();
    }

    public List<Pokemon> GetList()
    {
      return Pokemon.OrderBy(p => p.Name).ToList();
    }

    /// <summary>
    /// Redshift throws an exception using EF to add an entity so we use SQL here.
    /// </summary>
    /// <param name="pokemon"></param>
    /// <returns></returns>
    public Pokemon Create(Pokemon pokemon)
    {
      Database.ExecuteSqlCommand(
        DataContextHelper.InsertPokemonSql,
        new NpgsqlParameter("@id", pokemon.Id),
        new NpgsqlParameter("@name", pokemon.Name),
        new NpgsqlParameter("@typeid", pokemon.TypeId),
        new NpgsqlParameter("@base_hp", pokemon.HealthPoints),
        new NpgsqlParameter("@base_attack", pokemon.Attack),
        new NpgsqlParameter("@base_defense", pokemon.Defense),
        new NpgsqlParameter("@spec_attack", pokemon.SpecialAttack),
        new NpgsqlParameter("@spec_defense", pokemon.SpecialDefense),
        new NpgsqlParameter("@base_speed", pokemon.Speed)
      );

      return this.Pokemon.SingleOrDefault(p => p.Id == pokemon.Id);
    }

    public Pokemon Update(Pokemon pokemon)
    {
      base.Update(pokemon);
      SaveChanges();

      return pokemon;
    }

    public void Delete(int id)
    {
      var pokemon = new Pokemon
      {
          Id = id
      };
      Entry(pokemon).State = EntityState.Deleted;
      SaveChanges();
    }
  }
}
