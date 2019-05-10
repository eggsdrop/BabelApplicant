using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace babelhealth.Data
{
    /// <summary>
    /// Helper class for data contexts
    /// </summary>
    public class DataContextHelper
    {
        public static Dictionary<string, string> ConnectionStrings = new Dictionary<string, string>();

        public const string InsertPokemonSql = @"
insert into pokemon (id, name, typeid, base_hp, base_attack, base_defense, spec_attack, spec_defense, base_speed)
values (@id, @name, @typeid, @base_hp, @base_attack, @base_defense, @spec_attack, @spec_defense, @base_speed)";
    }
}
