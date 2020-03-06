export const addSavePageCommand = () => {
  Cypress.Commands.add(
    "savePage",
    ({}: ISavePage = {}): Chainable<Element> => {
      return cy.selectGovukButton().click();
    }
  );
};
