context("Layout - Footer", () => {
  before(() => {
    cy.startForm({ formName: "test" });
  });

  it("Matches snapshot", function() {
    cy.get("footer").matchImageSnapshot();
  });

  it("Contains a copyright logo", function() {
    cy.get("footer").find(".govuk-footer__licence-logo");
  });

  it("Contains a license description", function() {
    cy.get("footer").find(".govuk-footer__licence-description");
  });

  it("Contains Cookies link", function() {
    cy.get("footer")
      .contains("Cookies")
      .should("have.attr", "href")
      .should("be.eq", "https://www.gov.uk/help/cookies");
  });

  it("Contains Feedback link", function() {
    cy.get("footer")
      .contains("Feedback")
      .should("have.attr", "href")
      .should("be.eq", "."); // TODO change when feedback added
  });

  it("Contains Help link", function() {
    cy.get("footer")
      .contains("Help")
      .should("have.attr", "href")
      .should("be.eq", "https://www.gov.uk/help");
  });

  it("Contains Privacy Link", function() {
    cy.get("footer")
      .contains("Privacy")
      .should("have.attr", "href")
      .should("be.eq", "https://www.gov.uk/help/privacy-notice");
  });

  it("Contains Terms & conditions link", function() {
    cy.get("footer")
      .contains("Terms & Conditions")
      .should("have.attr", "href")
      .should("be.eq", "/TermsAndConditions");
  });
});
