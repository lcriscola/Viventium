@echo off
docker kill viventium
docker run --rm --name viventium -p  5297:8080 -e ASPNETCORE_ENVIRONMENT=Development  viventium:1.0.0
 