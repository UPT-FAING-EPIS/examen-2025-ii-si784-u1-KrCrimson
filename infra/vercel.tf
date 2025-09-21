terraform {
  required_providers {
    vercel = {
      source = "vercel/vercel"
      version = ">= 0.7.0"
    }
  }
}


resource "vercel_project" "frontend" {
  name      = "flight-frontend-app"
  framework = "react"
  root_directory = "frontend"
}

resource "vercel_project_environment_variable" "api_url" {
  project_id = vercel_project.frontend.id
  key        = "2DRzWHcswJTyTAXmRBBHzxzO"
  value      = "https://examenu1calidad.vercel.app"
  target     = ["production"]
}
