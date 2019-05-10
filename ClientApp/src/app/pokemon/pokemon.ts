/**
 * Represents a Pokemon creature
 * @class
 */
export class Pokemon {
    id: number;
    name: string;
    healthPoints: number;
    defense: number;
    attack: number;
    specialAttack: number;
    specialDefense: number;
    typeId: number;
    speed: number;

    constructor(data: any) {
        Object.assign(this, data);
    }

    isType(typeFlag: number) {
        return (this.typeId | typeFlag) === this.typeId;
    }
}

/**
 * Describes a type of Pokemon
 */
export interface PokemonType {
  id: number;
  name: string;
}

/**
 * Helper class to obtain the set of Pokemon types
 * @class
 */
export class PokemonTypes {
  getTypes(): PokemonType[] {
    return [
      { id: 1, name: 'Normal' },
      { id: 2, name: 'Fire' },
      { id: 4, name: 'Water' },
      { id: 8, name: 'Electric' },
      { id: 16, name: 'Grass' },
      { id: 32, name: 'Ice' },
      { id: 64, name: 'Fighting' },
      { id: 128, name: 'Poison' },
      { id: 256, name: 'Ground' },
      { id: 512, name: 'Flying' },
      { id: 1024, name: 'Psychic' },
      { id: 2048, name: 'Bug' },
      { id: 4096, name: 'Rock' },
      { id: 8192, name: 'Ghost' },
      { id: 16384, name: 'Dragon' },
      { id: 32768, name: 'Dark' },
      { id: 65536, name: 'Steel' },
      { id: 131072, name: 'Fairy' }
    ].sort(function (a, b) {
      if (a.name > b.name) {
        return 1;
      } else if (a.name < b.name) {
        return -1;
      } else {
        return 0;
      }
    });
  }
}
