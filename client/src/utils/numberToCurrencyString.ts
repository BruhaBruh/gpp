const numberToCurrencyString = (n: number) => {
  const hasAfter = n % 1 !== 0;
  const afterDot = hasAfter ? `.${(n % 1).toString().substring(2, 4)}` : "";
  const result = [
    afterDot,
    ...Math.floor(n / 1)
      .toString()
      .split("")
      .reverse()
      .map((v, i) => {
        if ((i + 1) % 3 === 0) {
          return ` ${v}`;
        }
        return v;
      }),
  ];
  return result.reverse().join("");
};

export default numberToCurrencyString;
