# KEDA-Fireworks-Demo
KEDA scalability demo

## Prerequisites

1. AKS cluster

2. Azure Storage Queue

3. Azure functions core tools: https://github.com/Azure/azure-functions-core-tools

4. Visual Studio Code.

5. Docker, kubectl and all connections already established.

## Preparations

1. Download Fireworks sample from SFM repository located here: https://github.com/Azure/service-fabric-mesh-preview/tree/fireworks/samples/src/fireworks

2. You can change backround picture and recompile/push new docker image to image registry of your choice or use mine (`grabarz/azure-fireworks-web:v1`) with Warsaw at night picture.

3. Deploy fireworks web [yaml located here](./yaml/fireworks-web.yaml) to your Kubernetes cluster (please update target image accordingly). It will create load balancer with public IP. You cn check your IP in few minutes after deployment with following command `kubectl get services`

4. Build console application from [sources](./src/SendToQueue) and update connections string to your Azure Storage Queue.

## Demo

1. Install KEDA to your cluster `funct kubernetes install --namespace keda`

2. Init your Azure Function from Visual Studio Code command palete. Use `Azure Functions: Crete New Project...` then select empty folder, `javascript`, `Azure Storage Queue trigger`, leave default value for name, `create new local app settings` and pick your storage queue from list.

3. Add docker support to your function with `func init --docker-only`

4. Update javascript code of your function with [following code snippet](./src/fireworks-worker/index.js).

5. Build and deploy your Function to Kubernetes with command `func kubernetes deploy --name YOUR_FUNC_NAME_HERE --registry YOUR_DOCKER_REGISTRY_USERNAME`

6. Open your `fireworks-web` IP in browser. Send messages to queue with `console application` and enjoy fireworks.

7. You can also constantly observe new functions pods added in kubernetes with command: `kubectl get pods -w`