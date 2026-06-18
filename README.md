# Currency Exchange Office

**Course:** Network Application Development
**Name and Surname:** Guvanchmyrat Esenov
**Student ID:** 58260

## About the project

A simple .NET application that simulates an online currency exchange office. The system uses live exchange rates from the National Bank of Poland (NBP) API.

A user can register an account, top up a PLN balance, and buy or sell currencies. All balances are kept per user.

## Main parts

- **WCF Service** — handles all the logic and talks to the NBP API.
- **WPF Client** — a graphical app the user interacts with (login window + main exchange window).
- **Console Client** — a simple text-based client (kept from earlier labs for reference).

## Technologies

- .NET Framework 4.8
- WCF (Windows Communication Foundation)
- WPF (Windows Presentation Foundation)
- Newtonsoft.Json
- NBP API (`http://api.nbp.pl`)

## How to run the project

1. Open the solution `CurrencyExchangeService.sln` in **Visual Studio 2026** (or Visual Studio 2022).
2. Make sure the project targets **.NET Framework 4.8**.
3. In Solution Explorer, right-click the **solution** → **Configure Startup Projects** → **Multiple startup projects**.
4. Set both `CurrencyExchangeService` and `CurrencyExchangeWpfClient` to **Start**, with the service listed first.
5. Press **F5** to run.
6. The WCF Test Client opens (the service is now running), followed by the Login window of the WPF client.
7. Register a new user, log in, top up PLN, and start exchanging currencies.

## Project structure

```
/currency-exchange-office
├── WCF-Service/             (WCF service source code)
├── Client-Application/      (WPF client + old console client)
├── Documentation/           (architecture notes)
└── README.md
```

## Features

- User registration and login
- Virtual PLN balance with top-up
- Live exchange rates from the National Bank of Poland
- Buy and sell currencies between any supported codes
- Per-currency balances displayed in the main window
