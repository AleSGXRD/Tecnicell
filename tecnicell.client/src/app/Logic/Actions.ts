export enum ActionsType{
    ACCESSORY = 0,
    BATTERY = 1,
    SCREEN = 2,
    PHONE = 3,
}

export function filterActions(actions: any, type:ActionsType) : any{
    return actions.sort((a: any, b: any) => {
         if (a.name < b.name) return -1;
         if (a.name > b.name) return 1;
         return 0; 
        }).filter(
        (action:any) => {
            if(type != ActionsType.PHONE){
                if(action.name == "Armado" || action.name == "Pieza Extraida" || action.name == "Pieza Puesta" || action.name == "Reparado")
                    return false
            }
            return true;
        });
}