context("Form Components - Text input (telephone)", () => {
  before(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Text input (telephone)"
    });
  });

  it("matches snapshot", function() {
    cy.get("form").matchImageSnapshot();
  });

  it("has correct attributes", function() {
    cy.formContainsInputWithAttributes({ attribute: "type", value: "tel" });
    cy.formContainsInputWithAttributes({
      attribute: "autocomplete",
      value: "tel"
    });
  });

  it("contains error when invalid", function() {
    cy.savePage();
    cy.selectGovukError().contains("Please enter a valid UK phone number");
  });
});
