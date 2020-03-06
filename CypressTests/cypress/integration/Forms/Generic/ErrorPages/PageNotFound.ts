context("Page Not Found", () => {
  before(function() {
    cy.visit("/page-does-not-exist", {});
  });

  it("has the correct header", function() {
    cy.get("main").contains("Page not found");
  });

  it("snapshots match", function() {
    cy.get("main").matchImageSnapshot();
  });
});
