export const addSelectGovukErrorCommand = () => {
  Cypress.Commands.add(
    "selectGovukError",
    (): Chainable<Element> => {
      return cy.get("form").find(".govuk-error-message");
    }
  );
};
