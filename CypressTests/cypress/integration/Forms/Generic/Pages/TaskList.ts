context("Task List", () => {
  before(function() {
    cy.startForm({ formName: "test" });
  });

  it("Task list has the correct page header", function() {
    cy.selectGovukHeading({ size: "xl" }).contains("Automated Testing Form");
  });

  it("Task list has a table of tasks", function() {
    cy.get("main")
      .find(".app-task-list")
      .should("have.descendants", ".app-task-list__section")
      .and("have.descendants", ".app-task-list__item")
      .and("have.descendants", ".app-task-list__task-name");
  });

  it("Tasks have links to the sections", function() {
    cy.get("main")
      .find(".app-task-list__task-name")
      .should("have.descendants", ".govuk-link");
  });

  it("When a section is complete a completed tag is shown", function() {
    cy.get("main")
      .find(".app-task-list")
      .should("not.have.descendants", ".app-task-list__task-completed");

    completeRegularTextInputSection();

    cy.get("main")
      .find(".app-task-list")
      .should("have.descendants", ".app-task-list__task-completed");
  });
});

function completeRegularTextInputSection() {
  cy.startTaskListSection({ sectionName: "Text input (regular)" });
  cy.selectGovukInput().type("This is some text to complete the page");
  cy.savePage();
}
