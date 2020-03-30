# Introduction 
Cheat sheet for adventureworks micrososervices demo case

# Kubernetes Cheat Sheet

## kubectl
- [Full doc](https://kubernetes.io/docs/reference/kubectl/overview/)

kubectl get pod

## install dashboard
see: https://github.com/kubernetes/dashboard


### get the token 
$TOKEN=((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]

## Installing nginx ingress controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.30.0/deploy/static/mandatory.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.30.0/deploy/static/provider/cloud-generic.yaml

# AKS Cheat sheet
- az aks get-credentials --resource-group microservice-rg --name adventureworks

## Attach Azure Container Registry to AKS
- az aks update -n adventureworks -g microservice-rg --attach-acr jbens


# Helm Cheat sheet
- kubectl create namespace adventureworks
- helm install adventureworks .\adventureworks\  --namespace adventureworks
- helm upgrade adventureworks .\adventureworks\  --namespace adventureworks
- helm delete adventureworks --namespace adventureworks
- helm -n adventureworks delete frontend-devops

# Istio cheat sheet
## Installing istio
- istioctl operator init
- istioctl manifest apply --set profile=demo
- istioctl manifest apply --set values.kiali.enabled=true  --set values.prometheus.enabled=true --set values.tracing.enabled=true --set addonComponents.grafana.enabled=true


- kubectl create namespace adventureworks 
- kubectl label namespace adventureworks istio-injection=enabled