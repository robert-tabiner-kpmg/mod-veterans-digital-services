declare namespace Cypress {
  interface Chainable {
    // setup
    startForm(options: IStartForm): Chainable<Element>;
    startTaskListSection(options: IStartTaskListSection): Chainable<Element>;

    // actions
    checkRadioInput(options: ICheckRadioInput): Chainable<Element>;
    savePage(options?: ISavePage): Chainable<Element>;

    // tests
    formContainsInputWithAttributes(
      options: IFormContainsInputWithAttributes
    ): Chainable<Element>;

    // selectors
    selectGovukInput(): Chainable<Element>;
    selectGovukButton(): Chainable<Element>;
    selectGovukError(): Chainable<Element>;
    selectGovukHeading(options: ISelectGovukHeading): Chainable<Element>;
  }
}

// 1 Setup
interface IStartForm {
  formName: "test" | "afip" | "afcs";
}

interface IStartTaskListSection {
  sectionName: string;
}

// 2 Actions
interface ICheckRadioInput {
  inputIndex: number;
}

interface ISavePage {}

// 3 Tests
interface IFormContainsInputWithAttributes {
  attribute: string;
  value: string;
}

// 4 Selectors
interface ISelectGovukHeading {
  size: "xl" | "l" | "m";
}
