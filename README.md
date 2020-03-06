# MOD Veterans Digital Service (Forms)

### App Overview

A detailed overview of this project can be found here: https://github.com/mod-veterans/digital-service-web-app/wiki

### Pre-requisites
Docker

Govuk notify - https://www.notifications.service.gov.uk/
This app uses govuk notify in order to send email communications. To get access to our notify instance, speak to the MOD Digitial forms service manager.

### Configuration

| Configuration Value                 | Description                                                                                                                                                        |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Email:ApiKey                        | Gov uk Notify Api secret key - https://docs.notifications.service.gov.uk/net.html#api-keys                                                                         |
| Email:AFIPTemplateId                | Template Id for AFIP Email - https://docs.notifications.service.gov.uk/net.html#send-an-email                                                                      |
| Email:AFIPEmailRecipient            | Email address the AFIP form submissions will be sent to                                                                                                            |
| Email:AFIPUserTemplateId            | Template Id for AFIP user confirmation Email                                                                                                                       |
| Email:AFCSTemplateId                | Template Id for AFCS Email                                                                                                                                         |
| Email:AFCSEmailRecipient            | Email address the AFCS form submissions will be sent to                                                                                                            |
| Email:AFCSUserTemplateId            | Template Id for AFCS user confirmation Email                                                                                                                       |
| Email:DevelopmentUserEmailRecipient | Email address to redirect user confirmations to on dev machines                                                                                                    |
| Redis:Uri                           | The URI for connecting to the redis cache (usually http://localhost:6379 for local development                                                                     |
| Forms:AllowTestForms                | Allow the use of non-production forms for test purposes                                                                                                            |
