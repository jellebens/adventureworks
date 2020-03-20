$tag="dev"

docker build -f ".\Adventureworks.Orders\Dockerfile" . -t adventureworks/orders:$tag