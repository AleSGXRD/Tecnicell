
export function calculeGains(currencies: any[], values: any[]){
    currencies?.forEach(currency => {
        currency.money =0;
        values?.forEach(value =>{
            currency.money += processHistory(value,currency);
        })
      })
    return currencies
}

function processHistory(history : any, currency: any) : number{
    if(history['saleCodeNavigation'] == undefined) return 0;
    if(history['actionHistory'] == undefined){
      if(history['saleCodeNavigation']['currencyCode'] == currency.value)
        return history['saleCodeNavigation']['cost']
    }
  
    if(history['actionHistory'] == 'Entrada')
      if(history['saleCodeNavigation']['currencyCode'] == currency.value)
        return -history['saleCodeNavigation']['cost']
    
    if(history['actionHistory'] == 'Salida')
      if(history['saleCodeNavigation']['currencyCode'] == currency.value)
        return history['saleCodeNavigation']['cost']

    return 0;
}