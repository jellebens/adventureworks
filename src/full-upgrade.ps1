param (
    [Parameter(ParameterSetName="x")][string]$command = "upgrade",
    [Parameter(ParameterSetName="x")][string]$component = "all"
 )

Write-Host $component;

$canary="false";

if($command -eq 'canary'){
    $canary = "true";
    $command='upgrade';
}

$tag=Get-Date -Format 'yymmdd.hh.mmss'; 
if('all','frontend' -contains $component.ToLower()){
    Write-Host "Deploying frontend version $tag"
    docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag --build-arg VERSION=$tag
    helm $command frontend .\Helm\adventureworks\charts\frontend --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary
}

if('all','orders' -contains $component.ToLower()){
    Write-Host "Deploying orders service version $tag"
    docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$tag --build-arg VERSION=$tag
    if($canary -eq "false"){
        helm $command service.orders .\Helm\adventureworks\charts\orders --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary
    }else {
        helm uninstall service.orders.canary --namespace adventureworks
        helm install service.orders.canary .\Helm\adventureworks\charts\orders --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary
    }
    
    
    

}