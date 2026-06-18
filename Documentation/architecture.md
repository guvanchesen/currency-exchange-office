# Project Architecture

**Course:** Network Application Development
**Project:** Currency Exchange Office System
**Name and Surname:** Guvanchmyrat Esenov
**Student no:** 58260

---

## What the project does

This project is a simple currency exchange office built with .NET. A user can ask the system to convert money from one currency to another, and the system uses live exchange rates from the National Bank of Poland (NBP) to do the math.

## Main parts

The project has two parts:

**1. WCF Service** — does all the work. It talks to the NBP API to get exchange rates and calculates the conversion.

**2. Console Client** — a small program the user interacts with. It asks the user for input and shows the result. It does not do any calculations itself — it just sends the request to the service.

## How it works

1. The user types an amount and two currency codes (for example, 100 USD to EUR) into the client.
2. The client sends this request to the WCF service over the network.
3. The service contacts the NBP API to get the current rates for both currencies.
4. The service calculates the result and sends it back to the client.
5. The client shows the result to the user.

```
Client  <-- WCF -->  Service  <-- HTTP -->  NBP API
```

## How the conversion works

NBP gives all rates in PLN. So to convert between two currencies, the service goes through PLN:

```
result = amount × (fromRate / toRate)
```

For example, to convert 100 USD to EUR:
- USD rate: 3.95 PLN
- EUR rate: 4.27 PLN
- Result: 100 × (3.95 / 4.27) ≈ 92.51 EUR

PLN itself has a rate of 1.

## Service methods

The service offers four methods:

- `GetExchangeRate(string)` — returns the PLN rate for one currency.
- `ExchangeCurrency(double, string, string)` — converts an amount between two currencies.
- `GetAvailableCurrencies()` — returns a list of supported currency codes.

## Technologies used

- **.NET Framework 4.8**
- **WCF** (Windows Communication Foundation) for the service
- **Newtonsoft.Json** for reading JSON from the NBP API
- **NBP API** (`http://api.nbp.pl`) for live exchange rates
- **Visual Studio 2026 Community**
- **Git / GitHub** for version control

## Error handling

The service checks for common problems:
- Invalid currency code (e.g. a typo) → returns a clear error message.
- Negative amounts → rejected.
- NBP API not reachable → reports the problem to the client.

## Project folders

```
/currency-exchange-office
├── WCF-Service/         (the service code)
├── Client-Application/  (the console client)
├── Documentation/       (this file)
└── README.md
```