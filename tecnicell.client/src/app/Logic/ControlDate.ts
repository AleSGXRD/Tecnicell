

export function generateDate(day:any, hours:any, minutes:any, seconds:any, time:any){
    // Ajuste de horas para formato 24 horas
    let adjustedHours = parseInt(hours);
    console.log(day,hours,minutes,seconds,time);
    if (time === 'pm' && parseInt(hours) < 12) {
        adjustedHours = parseInt(hours) + 12;
    } else if (time === 'am' && parseInt(hours) === 12) {
        adjustedHours = 0; // Medianoche
    }

    // Crear la fecha ajustada
    const today = new Date(); // Obtenemos la fecha de hoy
    console.log(day.getFullYear(),day.getMonth(), day.getDate());
    const adjustedDate = new Date(day.getFullYear(), day.getMonth(), day.getDate(), adjustedHours, parseInt(minutes), parseInt(seconds));

    // Mostrar la fecha ajustada
    console.log(adjustedDate);
    return adjustedDate;

}