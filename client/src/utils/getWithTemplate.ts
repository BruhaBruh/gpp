const getWithTemplate = (v: string, template: string): string => {
  const result = template.split("");

  v.split("")
    .reverse()
    .forEach((c, i) => (result[result.length - 1 - i] = c));
  return result.join("");
};

export default getWithTemplate;
