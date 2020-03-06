export const addFormContainsInputWithAttributesCommand = () => {
  Cypress.Commands.add(
    "formContainsInputWithAttributes",
    ({ attribute, value }: IFormContainsInputWithAttributes) => {
      cy.selectGovukInput()
        .should("have.attr", attribute)
        .should("be.eq", value);
    }
  );
};
