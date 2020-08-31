export const getInitials = (firstName: string, lastName: string) => {
  var name = `${firstName || 'A'} ${lastName}`
  var initials = name.match(/\b\w/g) || [];
  return ((initials.shift() || '') + (initials.pop() || '')).toUpperCase();
}

export const removeBetweenParenthesis = (text: string) => text.replace(/ *\([^)]*\) */g, "")

const escapeRegExp = (str: string) => str.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

export const startsWith = (value: string, keywords: string) => {
  return new RegExp(`\\b${escapeRegExp(keywords)}`, "i").test(value)
}
