context("Generic form - Radio input (regular)", () => {
  before(function() {
    cy.startForm({ formName: "test" }).startTaskListSection({
      sectionName: "Radio input (regular)"
    });
  });

  it("matches snapshot", function() {
    cy.get("form").matchImageSnapshot();
  });

  it("contains error when invalid", function() {
    cy.savePage();
    cy.selectGovukError().contains("An option is required");
  });
});
