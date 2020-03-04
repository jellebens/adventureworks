docker build -f ".\Adventureworks.Web\Dockerfile" . -t adventureworks/web:dev

 kubectl apply -f .\Adventureworks.Web\k8s.deploy.yaml