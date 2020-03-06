# Forms E2E Tests

## Getting Started

### 1. Install Dependencies

```
yarn install
```

### 2. Run Tests

in browser:

```
yarn browser
```

in terminal:

```
yarn terminal
```

---

## Configuration

The default configuration in cypress.json can be overriden by creating a 'cypress.env.json' file

| Name    | Description                      | Default                |
| ------- | -------------------------------- | ---------------------- |
| baseUrl | app url to run the tests against | https://localhost:5001 |

e.g. baseUrl can be overriden by adding the following contents to the json file:

```
{
    "baseUrl": "https://some-other-location/"
}
```
