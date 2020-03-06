// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

// -- Image Snapshots --
import { addMatchImageSnapshotCommand } from "cypress-image-snapshot/command";
import { addStartFormCommand } from "./commands/setup/startForm";
import { addCheckRadioInputCommand } from "./commands/actions/checkRadioInput";
import { addStartTaskListSectionCommand } from "./commands/setup/startTaskListSection";
import { addSavePageCommand } from "./commands/actions/savePage";
import { addFormContainsInputWithAttributesCommand } from "./commands/tests/formContainsInputWithAttributes";
import { addSelectGovukButtonCommand } from "./commands/selectors/selectGovukButton";
import { addSelectGovukInputCommand } from "./commands/selectors/selectGovukInput";
import { addSelectGovukErrorCommand } from "./commands/selectors/selectGovukError";
import { addSelectGovukHeadingCommand } from "./commands/selectors/selectGovukHeading";

// external
addMatchImageSnapshotCommand();

// commands > setup
addStartFormCommand();
addStartTaskListSectionCommand();

// commands > actions
addCheckRadioInputCommand();
addSavePageCommand();

// commands  > tests
addFormContainsInputWithAttributesCommand();

// commands > selectors
addSelectGovukButtonCommand();
addSelectGovukInputCommand();
addSelectGovukErrorCommand();
addSelectGovukHeadingCommand();
