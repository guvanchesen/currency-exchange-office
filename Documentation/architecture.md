Service: WCF service that exposes 4 methods over HTTP. Hosted in-process by the WCF Test Client (or self-host in IIS Express).
Client: console application that consumes the service via a generated proxy.
External API: NBP public API at api.nbp.pl, no authentication, JSON over HTTP.
Conversion logic: rates are fetched in PLN; cross-currency conversion is done by going through PLN.