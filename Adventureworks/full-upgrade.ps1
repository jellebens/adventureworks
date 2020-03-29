$tag=Get-Date -Format 'yymmddhhmmss'; 

Write-Host "Building containers"
docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag --build-arg VERSION=$tag
docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$tag --build-arg VERSION=$tag

Write-Host "Helm upgrade"
helm upgrade frontend .\Helm\adventureworks\charts\frontend --namespace adventureworks --set image.tag=$tag
helm upgrade service.orders .\Helm\adventureworks\charts\orders --namespace adventureworks --set image.tag=$tag
