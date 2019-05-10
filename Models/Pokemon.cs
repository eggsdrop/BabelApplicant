using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace babelhealth.Models
{
    /// <summary>
    /// Pokemon creature
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <value></value>
        [Column("id")]
        public int Id {get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <value></value>
        [Column("name")]
        public string Name {get; set;}

        /// <summary>
        /// Base health points
        /// </summary>
        /// <value></value>
        [Column("base_hp")]
        public short HealthPoints { get; set; }

        /// <summary>
        /// Base attack value
        /// </summary>
        /// <value></value>
        [Column("base_attack")]
        public short Attack { get; set; }

        /// <summary>
        /// Base defense value
        /// </summary>
        /// <value></value>
        [Column("base_defense")]
        public short Defense { get; set; }

        /// <summary>
        /// Special attack value
        /// </summary>
        /// <value></value>
        [Column("spec_attack")]
        public short SpecialAttack { get; set; }

        /// <summary>
        /// Special defense value
        /// </summary>
        /// <value></value>
        [Column("spec_defense")]
        public short SpecialDefense { get; set; }
        
        /// <summary>
        /// Speed
        /// </summary>
        /// <value></value>
        [Column("base_speed")]
        public short Speed { get; set; }

        /// <summary>
        /// Types represented as a "flags" int for UI purposes
        /// </summary>
        /// <value></value>
        [Column("typeid")]
        public int TypeId { get; set; }

        /// <summary>
        /// Pokemon types in enum form of this Pokemon
        /// </summary>
        /// <value></value>
        public PokemonTypes Types
        {
            get { return (PokemonTypes)Enum.ToObject(typeof(PokemonTypes), TypeId); }
        }
    }

    /// <summary>
    /// Types of Pokemon
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [Flags]
    public enum PokemonTypes
    {
        Normal = 1,
        Fire = 2,
        Water = 4,
        Electric = 8,
        Grass = 16,
        Ice = 32,
        Fighting = 64,
        Poison = 128,
        Ground = 256,
        Flying = 512,
        Psychic = 1024,
        Bug = 2048,
        Rock = 4096,
        Ghost = 8192,
        Dragon = 16384,
        Dark = 32768,
        Steel = 65536,
        Fairy = 131072
    }
}