context("Form Components - Text input (number)", () => {
  beforeEach(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Text input (number)"
    });
  });

  it("matches snapshot", function() {
    cy.get("form").matchImageSnapshot();
  });

  it("has correct attributes", function() {
    cy.formContainsInputWithAttributes({ attribute: "type", value: "number" });
  });

  it("does not allow text input", function() {
    cy.selectGovukInput()
      .type("abcd")
      .should("be.empty");
  });

  it("does allow number input", function() {
    cy.selectGovukInput()
      .type("1234")
      .invoke("val")
      .should("be.eq", "1234");
  });

  it("contains error when invalid", function() {
    cy.savePage();
    cy.selectGovukError().contains("Field is required");
  });
});
