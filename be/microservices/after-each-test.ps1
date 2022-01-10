#kubectl rollout restart deployment/ocelot-deployment
#kubectl rollout restart deployment/group-deployment
kubectl delete deployment group-deployment
kubectl delete deployment ocelot-deployment
#docker restart amiq-mssql-db
kubectl apply -f .\k8s\ocelot-depl.yaml
kubectl apply -f .\k8s\group-depl.yaml