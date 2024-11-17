export function getFirstDayOfWeek() {
    const now = new Date();
    const dayOfWeek = now.getDay(); // 0 (Sunday) to 6 (Saturday)
    const firstDay = new Date(now);

    // Assuming week starts on Monday. If Sunday is the first day of the week, use (dayOfWeek === 0 ? 6 : dayOfWeek - 1) instead
    const diff = now.getDate() - dayOfWeek + (dayOfWeek === 0 ? -6 : 1); 
    firstDay.setDate(diff);
    firstDay.setHours(0, 0, 0, 0); // Set to midnight

    return firstDay;
}

export function getLastDayOfWeek() {
    const now = new Date();
    const dayOfWeek = now.getDay(); // 0 (Sunday) to 6 (Saturday)
    const lastDay = new Date(now);

    // Assuming week ends on Sunday. If week ends on Saturday, use (6 - dayOfWeek) instead
    const diff = now.getDate() + (7 - dayOfWeek) % 7; 
    lastDay.setDate(diff);
    lastDay.setHours(23, 59, 59, 999); // Set to last minute of the day

    return lastDay;
}
