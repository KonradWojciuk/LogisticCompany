#!/bin/bash

# token JWT
token=$(curl -X POST -H "Content-Type: application/json" -d '{"username": "test", "password": "password"}' http://localhost:7103/api/auth/login | grep -o '"token":"[^"]*' | cut -d'"' -f4)

# Testowanie endpointu dla Shipments

# Pobierz wszystkie przesyłki
curl -X GET http://localhost:7103/api/shipment -H "Authorization: Bearer $token"

# Pobierz przesyłkę o określonym ID
curl -X GET http://localhost:7103/api/shipment/1 -H "Authorization: Bearer $token"

# Pobierz przesyłki przypisane do określonego samochodu
curl -X GET http://localhost:7103/api/shipment/truck/1 -H "Authorization: Bearer $token"

# Pobierz przesyłki przypisane do określonego magazynu
curl -X GET http://localhost:7103/api/shipment/warehouse/1 -H "Authorization: Bearer $token"

# Stwórz nową przesyłkę
curl -X POST -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/shipment -H "Authorization: Bearer $token"

# Aktualizuj istniejącą przesyłkę
curl -X PUT -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/shipment/1 -H "Authorization: Bearer $token"

# Usuń przesyłkę o określonym ID
curl -X DELETE http://localhost:7103/api/shipment/1 -H "Authorization: Bearer $token"

# Testowanie endpointu dla Trucks

# Pobierz wszystkie samochody
curl -X GET http://localhost:7103/api/truck -H "Authorization: Bearer $token"

# Pobierz samochód o określonym ID
curl -X GET http://localhost:7103/api/truck/1 -H "Authorization: Bearer $token"

# Pobierz samochód po numerze rejestracyjnym
curl -X GET http://localhost:7103/api/truck/licensePlate/ABC123 -H "Authorization: Bearer $token"

# Pobierz samochód po numerze karty kierowcy
curl -X GET http://localhost:7103/api/truck/driverCard/123456 -H "Authorization: Bearer $token"

# Stwórz nowy samochód
curl -X POST -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/truck -H "Authorization: Bearer $token"

# Aktualizuj istniejący samochód
curl -X PUT -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/truck/1 -H "Authorization: Bearer $token"

# Usuń samochód o określonym ID
curl -X DELETE http://localhost:7103/api/truck/1 -H "Authorization: Bearer $token"

# Testowanie endpointu dla Warehouses

# Pobierz wszystkie magazyny
curl -X GET http://localhost:7103/api/warehouse -H "Authorization: Bearer $token"

# Pobierz magazyn o określonym ID
curl -X GET http://localhost:7103/api/warehouse/1 -H "Authorization: Bearer $token"

# Pobierz magazyny w określonym kraju
curl -X GET http://localhost:7103/api/warehouse/country/Poland -H "Authorization: Bearer $token"

# Pobierz magazyny w określonym mieście
curl -X GET http://localhost:7103/api/warehouse/city/Warsaw -H "Authorization: Bearer $token"

# Stwórz nowy magazyn
curl -X POST -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/warehouse -H "Authorization: Bearer $token"

# Aktualizuj istniejący magazyn
curl -X PUT -H "Content-Type: application/json" -d '{"propertyName": "propertyValue"}' http://localhost:7103/api/warehouse/1 -H "Authorization: Bearer $token"

# Usuń magazyn o określonym ID
curl -X DELETE http://localhost:7103/api/warehouse/1 -H "Authorization: Bearer $token"
