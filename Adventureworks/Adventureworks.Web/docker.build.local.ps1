$tag=Get-Date -Format 'yymmddhhmmss'; 

docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag
