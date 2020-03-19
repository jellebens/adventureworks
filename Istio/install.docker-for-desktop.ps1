helm repo add istio.io https://storage.googleapis.com/istio-release/releases/1.5.0/charts/

kubectl create namespace istio-system

helm template istio-init C:\Users\jelle\Downloads\istio-1.5.0\install/kubernetes/helm/istio-init  --namespace istio-system | kubectl apply -f -

kubectl -n istio-system wait --for=condition=complete job --all

helm template istio C:\Users\jelle\Downloads\istio-1.5.0\install/kubernetes/helm/istio --namespace istio-system --values C:\Users\jelle\Downloads\istio-1.5.0\install\kubernetes\helm\istio\values-istio-minimal.yaml | kubectl apply -f -