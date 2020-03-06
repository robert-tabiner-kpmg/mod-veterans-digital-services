export const addCheckRadioInputCommand = () => {
  Cypress.Commands.add(
    "checkRadioInput",
    ({ inputIndex }: ICheckRadioInput): Chainable<Element> => {
      return cy
        .get("form")
        .find(".govuk-radios__input")
        .eq(inputIndex)
        .check();
    }
  );
};
