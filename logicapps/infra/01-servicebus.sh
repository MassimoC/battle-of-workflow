#!/bin/bash
echo "... Loading variables"
. ./variables.sh

echo ".................................................." 
echo ">>> Creating resources for [$INFRACODE] <<<"
echo ".................................................." 

echo "${DBG}... Create resource group [$RESOURCE_GROUP]"
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "${DBG}... Create servicebus namespace [$SERVICEBUS_NAMESPACE]"
az servicebus namespace create --resource-group $RESOURCE_GROUP --name $SERVICEBUS_NAMESPACE --location $LOCATION

echo "${DBG}... Create servicebus topic [$SERVICEBUS_TOPIC]"
az servicebus topic create --resource-group $RESOURCE_GROUP --namespace-name $SERVICEBUS_NAMESPACE --name $SERVICEBUS_TOPIC

echo "${DBG}... Set authorization"
az servicebus namespace authorization-rule keys list --resource-group $RESOURCE_GROUP --namespace-name $SERVICEBUS_NAMESPACE --name RootManageSharedAccessKey --query primaryConnectionString --output tsv
