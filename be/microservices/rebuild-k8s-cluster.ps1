kubectl delete --all deployments
kubectl delete --all services

#docker rm -f $(docker ps -a -q --filter="ancestor=<image id>")

#docker rmi amiqservicesgroup
#docker rmi amiqservicesuser
#docker rmi amiqservicesfriendship

docker build -t amiqservicesuser -f .\Amiq.Services\Amiq.Services.User\Amiq.Services.User\Dockerfile .\Amiq.Services\Amiq.Services.User
docker build -t amiqservicesgroup -f .\Amiq.Services\Amiq.Services.Group\Amiq.Services.Group\Dockerfile .\Amiq.Services\Amiq.Services.Group
docker build -t amiqservicesfriendship -f .\Amiq.Services\Amiq.Services.Friendship\Amiq.Services.Friendship\Dockerfile .\Amiq.Services\Amiq.Services.Friendship

docker build -t amiq-ocelot-gateway -f .\Amiq.ApiGateways\Amiq.ApiGateways.WebApp\Amiq.ApiGateways.WebApp\Dockerfile .\Amiq.ApiGateways\Amiq.ApiGateways.WebApp


kubectl apply -f .\k8s\user-depl.yaml
kubectl apply -f .\k8s\group-depl.yaml
kubectl apply -f .\k8s\friendship-depl.yaml

kubectl apply -f .\k8s\ocelot-np-srv.yaml
kubectl apply -f .\k8s\ocelot-depl.yaml