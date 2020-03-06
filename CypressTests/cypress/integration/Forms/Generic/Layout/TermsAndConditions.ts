context("Layout - Terms and Conditions", () => {
  context("(pre-login)", function() {
    before(function() {
      cy.visit("/?form=Test", {});
      cy.get("#terms-and-conditions").click();
    });
    runTest();
  });

  context("(post-login)", function() {
    before(function() {
      cy.startForm({ formName: "test" });
      cy.get("#terms-and-conditions").click();
    });
    runTest();
  });
});

function runTest() {
  it("Header is correct", function() {
    cy.get("main")
      .find(".govuk-heading-xl")
      .should("contain", "Terms and Conditions");
  });

  it("Sub header is correct", function() {
    cy.get("main")
      .find(".govuk-heading-m")
      .should("contain", "This is the Terms & Conditions page");
  });

  it("matches snapshot", function() {
    cy.get("main").matchImageSnapshot();
  });
}
