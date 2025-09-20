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

resource "azurerm_postgresql_server" "db" {
  name                = "flightdbserver"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  administrator_login = "adminuser"
  administrator_login_password = "AdminPassword123!"
  sku_name            = "B_Gen5_1"
  storage_mb          = 5120
  version             = "11"
  auto_grow_enabled   = true
  backup_retention_days = 7
  geo_redundant_backup_enabled = false
  public_network_access_enabled = true
}

resource "azurerm_postgresql_database" "db" {
  name                = "flightdb"
  resource_group_name = azurerm_resource_group.rg.name
  server_name         = azurerm_postgresql_server.db.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "azurerm_app_service_plan" "asp" {
  name                = "flight-appserviceplan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
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
    dotnet_framework_version = "v6.0"
  }
  app_settings = {
    "DB_CONNECTION" = "Server=${azurerm_postgresql_server.db.fqdn};Database=flightdb;User Id=adminuser;Password=AdminPassword123!;"
  }
}

resource "azurerm_web_app" "frontend" {
  name                = "flight-frontend-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.asp.id
  site_config {
    node_version = "16-lts"
  }
}
