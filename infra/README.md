# Infraestructura IaC (Terraform)

## Despliegue en Azure

1. Instala Terraform y Azure CLI.
2. Configura tus credenciales de Azure (`az login`).
3. Edita los archivos `main.tf` y `variables.tf` si necesitas personalizar nombres o credenciales.
4. Ejecuta:
   ```sh
   terraform init
   terraform plan
   terraform apply
   ```
5. Revisa los outputs para obtener las URLs de los servicios.

## Despliegue en Vercel

- El frontend se despliega automáticamente con el workflow de Github Actions.
- Configura los secretos en Github (`VERCEL_TOKEN`, `VERCEL_ORG_ID`, `VERCEL_PROJECT_ID`).
- La URL pública se mostrará en los outputs de Vercel y en la web de Vercel.

## Notas
- Para backend económico, considera Railway o Heroku.
- Para base de datos económica, considera Railway, Heroku o Supabase.
- Elimina recursos no usados para evitar costos.
