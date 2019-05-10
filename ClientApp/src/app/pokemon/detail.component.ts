import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiResponse } from '../common/apiresponse';
import { PokemonService } from './pokemon.service';
import { Pokemon, PokemonTypes } from './pokemon';

/**
 * Component to add/edit Pokemon
 * @class
 */
@Component({
    selector: 'app-pokemeon-detail',
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.css']
})
export class DetailComponent {
    private subscription: any;

    public action: string;
    public isEdit: boolean;
    public response: ApiResponse;
    public model: Pokemon;
    public pokemonTypes: PokemonTypes;

    constructor(private router: Router, private route: ActivatedRoute, private pokemonService: PokemonService) {
    }

    ngOnInit() {
        var me = this;
        me.pokemonTypes = new PokemonTypes();
        me.subscription = me.route.params.subscribe(params => {
            let id = params['id'];
            if (id === 'add') {
                me.action = null;
                me.isEdit = false;
                me.response = null;
                me.model = new Pokemon({});
            } else {
                me.pokemonService.getById(id).subscribe(result => {
                        console.log('Pokemon detail result', result, this);
                        me.action = params['action'];
                        me.isEdit = true;
                        me.response = result;
                        me.model =  new Pokemon(result.data);
                    },
                    error =>  {
                        console.log(error)
                    });
            }
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    onSave() {
        var me = this,
            isEdit = this.isEdit;

        me.getSaveMethod(isEdit)
            .subscribe(
                successResponse => {
                    me.response = Object.assign(new ApiResponse(), successResponse);
                    if (me.response.isSuccess()) {
                        if (!isEdit) {
                            this.router.navigate(['/detail/'+me.response.data.id+'/', {action: 'added'}]);
                        } else {
                            me.action = 'updated';
                            me.model =  new Pokemon(me.response.data);
                        }
                    }
                },
                failureResponse => {
                    console.log("Saving pokemon failed", failureResponse);
                }
            );
    }

    onTypeChange(typeFlag: number) {
        console.log('onTypeChange', typeFlag);
        let appliedTypes = this.model.typeId | typeFlag;

        if (appliedTypes !== this.model.typeId) {
            this.model.typeId = appliedTypes;
        } else {
            this.model.typeId -= typeFlag;
        }
    }

    getSaveMethod(isEdit: boolean) {
        var model = this.model;

        if (isEdit) {
            return this.pokemonService.update(model);
        } else {
            return this.pokemonService.create(model);
        }
    }

    onDismissAlert() {
        this.action = null;
    }
}
