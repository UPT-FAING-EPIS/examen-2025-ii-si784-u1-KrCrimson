output "resource_group_name" {
  value = azurerm_resource_group.rg.name
}

output "backend_url" {
  value = azurerm_web_app.backend.default_hostname
}

output "frontend_url" {
  value = azurerm_web_app.frontend.default_hostname
}

output "postgresql_server" {
  value = azurerm_postgresql_flexible_server.db.fqdn
}
