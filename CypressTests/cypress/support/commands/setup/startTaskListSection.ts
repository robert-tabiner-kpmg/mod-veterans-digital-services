export const addStartTaskListSectionCommand = () => {
  Cypress.Commands.add(
    "startTaskListSection",
    ({ sectionName }: IStartTaskListSection): Chainable<Element> => {
      return cy
        .get("main")
        .contains(sectionName)
        .click();
    }
  );
};
