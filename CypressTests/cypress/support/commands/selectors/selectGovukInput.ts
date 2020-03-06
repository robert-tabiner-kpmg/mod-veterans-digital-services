export const addSelectGovukInputCommand = () => {
  Cypress.Commands.add(
    "selectGovukInput",
    (): Chainable<Element> => {
      return cy.get("form").find(".govuk-input");
    }
  );
};
