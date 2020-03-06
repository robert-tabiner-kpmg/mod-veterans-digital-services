context("Layout - Header", () => {
  before(() => {
    cy.startForm({ formName: "test" });
  });

  context("Header", function() {
    it("Page contains header", function() {
      cy.get("header");
    });

    it("Snapshots match", function() {
      cy.get("header").matchImageSnapshot();
    });

    it("Contains the gov.uk link", function() {
      cy.get("header")
        .find(".govuk-header__logo")
        .find(".govuk-header__link--homepage")
        .should("have.attr", "href")
        .should("be.eq", "https://www.gov.uk");
    });

    it("Contains the service name", function() {
      cy.get("header")
        .find(".govuk-header__content")
        .find(".govuk-header__link--service-name")
        .should("have.attr", "href")
        .should(
          "be.eq",
          "https://www.gov.uk/government/organisations/veterans-uk"
        );
    });
  });
  context("Phase banner", function() {
    it("Page contains phase banner", function() {
      cy.get("body")
        .find(".govuk-phase-banner")
        .should(
          "contain",
          "This is a new service – your feedback will help us to improve it."
        );
    });

    it("Snapshots match", function() {
      cy.get("body")
        .find(".govuk-phase-banner")
        .matchImageSnapshot();
    });

    it("Contains alpha tag", function() {
      cy.get("body")
        .find(".govuk-phase-banner")
        .find(".govuk-phase-banner__content__tag")
        .contains("alpha");
    });

    it("Contains a clickable feedback link", function() {
      cy.get("body")
        .find(".govuk-phase-banner")
        .find(".govuk-link")
        .should("have.attr", "href")
        .should("be.eq", "#"); // TODO correct url here
    });
  });
});
