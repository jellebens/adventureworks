Write-Host "Installing Dashboard"
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.0.0-rc6/aio/deploy/recommended.yaml

$TOKEN=((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]
Write-Host "Token to access dashboard $TOKEN"

Write-Host "Installing Istio"
istioctl operator init
istioctl manifest apply --set profile=demo


Write-Host "Creating Adventureworks namespace"
kubectl create namespace adventureworks
kubectl label namespace adventureworks istio-injection=enabled