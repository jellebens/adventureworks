$tag=(date).ToString('yyyyMMddHHmmssfff')

docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag

 kubectl set image deployment/adventureworks web=adventureworks/web:$tag