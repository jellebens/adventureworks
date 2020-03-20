$tag="dev"

docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag