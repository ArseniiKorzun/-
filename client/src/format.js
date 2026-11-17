// All session times are stored and displayed in UTC. The API returns ISO strings ending in "Z",
// so the UTC date and time can be read directly from the string without browser time-zone conversion.
export const TIME_ZONE_NOTE = 'All times are in UTC.'

export const datePart = (iso) => iso.slice(0, 10)
export const timePart = (iso) => iso.slice(11, 16)
export const toInputValue = (iso) => iso.slice(0, 16)
export const fromInputValue = (value) => `${value}:00Z`
