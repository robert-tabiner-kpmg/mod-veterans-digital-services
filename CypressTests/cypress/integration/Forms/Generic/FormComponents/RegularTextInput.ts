context("Form Components - Text input (regular)", () => {
  before(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Text input (regular)"
    });
  });

  it("snapshots match", function() {
    cy.get("main").matchImageSnapshot();
  });

  it("contains error when invalid", function() {
    cy.savePage();
    cy.selectGovukError().contains("Field is required");
  });
});
