export const addSelectGovukButtonCommand = () => {
  Cypress.Commands.add(
    "selectGovukButton",
    (): Chainable<Element> => {
      return cy.get("form").find(".govuk-button");
    }
  );
};
