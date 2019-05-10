import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pokemon } from './pokemon';
import { ApiResponse } from '../common/apiresponse';

/**
 * Service for Pokemon API calls
 * @class
 */
@Injectable()
export class PokemonService {
    http: HttpClient;
    baseApiUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseApiUrl = baseUrl + 'api/PokeApi/';
    }

    /**
     * Retrieves a list of Pokemon
     */
    getList() {
        return this.http.get<ApiResponse>(this.getUrl('Get'));
    }

    /**
     * Retrieves a Pokemon
     * @param id The ID, or National Number, of the Pokemon
     */
    getById(id: number) {
        return this.http.get<ApiResponse>(this.getUrl('Get/' + id));
    }

    /**
     * Deletes a Pokemon
     * @param id The ID, or National Number, of the Pokemon
     */
    delete(id: number) {
        return this.http.delete<ApiResponse>(this.getUrl('Delete/' + id));
    }

    /**
     * Creates a new Pokemon
     * @param pokemon The Pokemon to create
     */
    create(pokemon: Pokemon) {
        return this.http.put<ApiResponse>(this.getUrl('Put'), pokemon);
    }

    /**
     * Updates a Pokemon
     * @param pokemon The Pokemon to update
     */
    update(pokemon: Pokemon) {
        return this.http.post<ApiResponse>(this.getUrl('Post'), pokemon)
    }

    /**
     * Helper to build URL's to call
     * @private
     * @param suffix Suffix for the URL to call
     */
    private getUrl(suffix: string) {
        return this.baseApiUrl + suffix;
    }
}
