param (
    [string]$command = "upgrade", 
    [string]$component = "all",
    [string]$canary="false"
 )

Write-Host $component;

$helmInstalls =  helm list -n adventureworks -o json | ConvertFrom-Json


if('all','frontend' -contains $component.ToLower()){
    $install = "frontend"
    $helm = $helmInstalls |  Where-Object {$_.name -eq $install }
    $rev = ($helm.revision -as [int]) + 1
    $tag = "$($helm.app_version).$rev";

    Write-Host "Deploying frontend version $tag"
    
    docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:$tag --build-arg VERSION=$tag
    helm $command $install .\Helm\adventureworks\charts\frontend --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary
}

if('all','orders' -contains $component.ToLower()){
    $install = "service.orders"
    $helm = $helmInstalls |  Where-Object {$_.name -eq $install }
    
    if($canary -eq "false"){
        $rev = ($helm.revision -as [int]) + 1
        $tag = "$($helm.app_version).$($rev)";
        Write-Host "Deploying new orders service version $tag"
        docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$tag --build-arg VERSION=$tag

        helm $command $install .\Helm\adventureworks\charts\orders --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary
    }else {
        $rev = ($helm.revision -as [int])
        $tag = "$($helm.app_version).$($rev)";
        $canaryTag = "$($helm.app_version).$($rev + 1)";
        
        Write-Host "Deploying canary orders service version $canaryTag"
        docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$canaryTag --build-arg VERSION=$canaryTag
        helm $command $install .\Helm\adventureworks\charts\orders --namespace adventureworks --set image.tag=$tag --set canary.enabled=$canary --set canary.tag=$canaryTag
    }
    
    
    

}