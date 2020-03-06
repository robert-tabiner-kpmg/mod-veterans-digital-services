export const addStartFormCommand = () => {
  Cypress.Commands.add(
    "startForm",
    ({ formName }: IStartForm): Chainable<Element> => {
      return cy
        .clearCookies()
        .visit(`/?form=${formName}`, {})
        .get("#start-form")
        .click();
    }
  );
};
