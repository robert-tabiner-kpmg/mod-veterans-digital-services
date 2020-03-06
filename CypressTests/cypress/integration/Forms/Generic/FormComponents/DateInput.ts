context("Form Components - Date Input", () => {
  before(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Date input"
    });
  });

  it("matches snapshot", function() {
    cy.get("form").matchImageSnapshot();
  });

  it("has correct attributes", function() {
    cy.formContainsInputWithAttributes({ attribute: "type", value: "number" });
  });

  it("contains error when empty", function() {
    cy.savePage();
    cy.selectGovukError().contains("Error: Enter a date");
  });
});

context("Form Components - Date Input Validation", () => {
  beforeEach(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Date input"
    });
  });

  it("contains error when invalid day is provided", function() {
    cy.get(".govuk-input").eq(0).type("32");
    cy.get(".govuk-input").eq(1).type("12");
    cy.get(".govuk-input").eq(2).type("2001");
    cy.savePage();

    cy.selectGovukError().contains("Enter a valid date like 31 03 1980");
  });

  it("contains error when invalid month is provided", function() {
    cy.get(".govuk-input").eq(0).type("25");
    cy.get(".govuk-input").eq(1).type("13");
    cy.get(".govuk-input").eq(2).type("2001");
    cy.savePage();

    cy.selectGovukError().contains("Enter a valid date like 31 03 1980");
  });

  it("contains error when invalid year is provided", function() {
    cy.get(".govuk-input").eq(0).type("25");
    cy.get(".govuk-input").eq(1).type("12");
    cy.get(".govuk-input").eq(2).type("300");
    cy.savePage();

    cy.selectGovukError().contains("Enter a valid date like 31 03 1980");
  });
});
