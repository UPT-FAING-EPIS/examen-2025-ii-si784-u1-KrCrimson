terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.0.0"
    }
  }
  required_version = ">= 1.3.0"
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "flight-reservation-rg"
  location = "East US"
}


resource "azurerm_postgresql_flexible_server" "db" {
  name                   = "flightdbserver"
  location               = azurerm_resource_group.rg.location
  resource_group_name    = azurerm_resource_group.rg.name
  administrator_login    = "adminuser"
  administrator_password = "AdminPassword123!"
  sku_name               = "B_Standard_B1ms"
  storage_mb             = 32768
  version                = "13"
  zone                   = "1"
  backup_retention_days  = 7
  public_network_access_enabled = true
}


resource "azurerm_postgresql_flexible_database" "db" {
  name                = "flightdb"
  resource_group_name = azurerm_resource_group.rg.name
  server_name         = azurerm_postgresql_flexible_server.db.name
  charset             = "UTF8"
  collation           = "en_US.utf8"
}


resource "azurerm_app_service_plan" "asp" {
  name                = "flight-appserviceplan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Linux"
  reserved            = true
  sku {
    tier = "Basic"
    size = "B1"
  }
}


resource "azurerm_web_app" "backend" {
  name                = "flight-backend-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.asp.id
  site_config {
    linux_fx_version = "DOTNETCORE|6.0"
  }
  app_settings = {
    "DB_CONNECTION" = "Server=${azurerm_postgresql_flexible_server.db.fqdn};Database=flightdb;User Id=adminuser;Password=AdminPassword123!;"
  }
}


resource "azurerm_web_app" "frontend" {
  name                = "flight-frontend-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.asp.id
  site_config {
    linux_fx_version = "NODE|16-lts"
  }
}
