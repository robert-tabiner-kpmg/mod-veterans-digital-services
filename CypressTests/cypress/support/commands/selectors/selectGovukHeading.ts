export const addSelectGovukHeadingCommand = () => {
  Cypress.Commands.add(
    "selectGovukHeading",
    (options: ISelectGovukHeading): Chainable<Element> => {
      return cy.get("main").find(`.govuk-heading-${options.size}`);
    }
  );
};
