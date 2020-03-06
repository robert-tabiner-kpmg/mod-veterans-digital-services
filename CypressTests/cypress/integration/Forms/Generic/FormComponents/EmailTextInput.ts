context("Form Components - Text input (email)", () => {
  before(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Text input (email)"
    });
  });

  it("matches snapshot", function() {
    cy.get("form").matchImageSnapshot();
  });

  it("has correct attributes", function() {
    cy.formContainsInputWithAttributes({ attribute: "type", value: "email" });
    cy.formContainsInputWithAttributes({
      attribute: "autocomplete",
      value: "email"
    });
  });

  it("contains error when invalid", function() {
    cy.savePage();
    cy.selectGovukError().contains("Enter an email address in the correct format, like name@example.com");
  });
});
