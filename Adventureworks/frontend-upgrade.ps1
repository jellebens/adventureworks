$tag=Get-Date -Format 'yymmddhhmmss'; 

docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag --build-arg VERSION=$tag
docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$tag --build-arg VERSION=$tag

Write-Host "Helm upgrade"
helm upgrade adventureworks .\Helm\adventureworks\ --namespace adventureworks --set frontend.image.tag=$tag --set orders.image.tag=$tag
