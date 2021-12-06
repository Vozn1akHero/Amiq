kubectl delete --all deployments


kubectl apply -f .\k8s\group-depl.yaml


kubectl apply -f .\k8s\ocelot-np-srv.yaml
kubectl apply -f .\k8s\ocelot-depl.yaml