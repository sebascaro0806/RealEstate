#!/bin/bash

# Aplicar migraciones
dotnet ef database update

# Iniciar la aplicación
dotnet run