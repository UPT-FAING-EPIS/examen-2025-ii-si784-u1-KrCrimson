variable "location" {
  description = "Azure region"
  type        = string
  default     = "East US"
}

variable "db_admin" {
  description = "DB admin username"
  type        = string
  default     = "adminuser"
}

variable "db_password" {
  description = "DB admin password"
  type        = string
  default     = "AdminPassword123!"
}
