#!/bin/bash

# Aplicar migraciones
dotnet ef database update

# Iniciar la aplicaci�n
dotnet run