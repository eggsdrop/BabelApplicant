import { Component } from '@angular/core';
import { PokemonService } from './pokemon.service';
import { ApiResponse } from '../common/apiresponse';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ConfirmComponent } from '../common/confirm.component';
import { Pokemon, PokemonTypes } from './pokemon';

/**
 * Component to view the list of Pokemon
 * @class
 */
@Component({
    selector: 'app-pokemeon-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent {
    public response: ApiResponse;
    public pokemonTypes: PokemonTypes;

    constructor(private pokemonService: PokemonService,
                private dialog: MatDialog) {
        this.pokemonTypes = new PokemonTypes();
        this.onLoadList();
    }

    onDelete(id: number, name: string) {
        let me = this,
            dialogConfig = this.getDialogConfig(name),
            dialog = this.dialog.open(ConfirmComponent, dialogConfig);

        dialog.afterClosed().subscribe(result => {
            if (result === true) { // dialog was confirmed (not dismissed)
                me.response = null;
                me.pokemonService
                    .delete(id)
                    .subscribe(
                        successResponse => {
                            me.response = Object.assign(new ApiResponse(), successResponse);
                        },
                        failureResponse => {
                            console.log("DELETE failed", failureResponse);
                        }
                    );
            }
        });
    }

    onLoadList() {
        let me = this;
        me.pokemonService.getList().subscribe(response => {
            console.log('Pokemon list result', response);
            me.response = Object.assign(new ApiResponse(), response);
            me.response.data = response.data.map(p => new Pokemon(p));
        }, error => console.error(error));
    }

    private getDialogConfig(name: string) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.data = {
            action: 'delete',
            content: name
        };

        return dialogConfig;
    }
}
