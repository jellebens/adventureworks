# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references


If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)


# Kubernetes Cheat Sheet

## kubectl
- [Full doc](https://kubernetes.io/docs/reference/kubectl/overview/)

kubectl get pod

## install dashboard
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v1.10.1/src/deploy/recommended/kubernetes-dashboard.yaml

### get the token 
$TOKEN=((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]

## Installing nginx ingress controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.30.0/deploy/static/mandatory.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.30.0/deploy/static/provider/cloud-generic.yaml

# AKS Cheat sheet
## Attach Azure Container Registry to AKS

az aks update -n adventureworks -g microservice-rg --attach-acr jbens

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



- kubectl label namespace adventureworks istio-injection=enabled