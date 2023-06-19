#!/bin/bash
echo "... Loading variables"
. ./variables.sh

echo ".................................................." 
echo ">>> Supporting infra for [$INFRACODE] <<<"
echo ".................................................." 

echo "${DBG}... Create log analytics workspace [$LOG_ANALYTICS_WORKSPACE]"
RES=$(az monitor log-analytics workspace create \
  --resource-group "$RESOURCE_GROUP" \
  --location "$LOCATION" \
  --workspace-name "$LOG_ANALYTICS_WORKSPACE")

echo "${DBG}... Retrieving log analytics client id"
LOG_ANALYTICS_WORKSPACE_CLIENT_ID=`az monitor log-analytics workspace show  \
  --resource-group "$RESOURCE_GROUP" \
  --workspace-name "$LOG_ANALYTICS_WORKSPACE" \
  --query customerId  \
  --output tsv | tr -d '[:space:]'`

echo "${OUT}... ... $LOG_ANALYTICS_WORKSPACE_CLIENT_ID"

echo "${DBG}... Retrieving log analytics secret"
LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET=`az monitor log-analytics workspace get-shared-keys \
  --resource-group "$RESOURCE_GROUP" \
  --workspace-name "$LOG_ANALYTICS_WORKSPACE" \
  --query primarySharedKey \
  --output tsv | tr -d '[:space:]'`

echo "${OUT}... ... $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET"

echo "${DBG}... Creating Application Insights [$APP_INS]"
RES=$(az monitor app-insights component create --app $APP_INS \
  --location "$LOCATION" \
  -g "$RESOURCE_GROUP" \
   --workspace "$LOG_ANALYTICS_WORKSPACE")

AI_ID=$(az resource show --name $APP_INS -g "$RESOURCE_GROUP" --resource-type 'microsoft.insights/components' -o tsv --query id | tr -d '[:space:]')
echo "${OUT}... ... $AI_ID"

AI_KEY=$(az resource show --name $APP_INS -g "$RESOURCE_GROUP" --resource-type 'microsoft.insights/components' -o tsv --query properties.InstrumentationKey | tr -d '[:space:]')
echo "${OUT}... ... $AI_KEY"

# ********************************************
# *****Storage account
# ******************************************** 
echo "${DBG}... Creating storage account [$STORAGE_ACCOUNT]" 

RES=$(az storage account create --name $STORAGE_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION \
  --sku Standard_LRS)

echo "${DBG}... Retrieving storage connection string"
STORAGE_CONNECTION_STRING=$(az storage account show-connection-string --name $STORAGE_ACCOUNT -g $RESOURCE_GROUP -o tsv)
echo "${OUT}... ... $STORAGE_CONNECTION_STRING"

echo "${DBG}... Retrieving storage key" 
STORAGE_KEY=$(az storage account keys list -g $RESOURCE_GROUP -n $STORAGE_ACCOUNT --query '[0].value' -o tsv) 
echo "${OUT}... ... $STORAGE_KEY"

echo "${DBG}... Create logicapps plan [$LOGICAPPS_PLAN]"
az appservice plan create -g $RESOURCE_GROUP -n $LOGICAPPS_PLAN --sku WS1

echo "${DBG}... Create logicapps [$LOGICAPPS_NAME]"
az logicapp create --name $LOGICAPPS_NAME \
    --resource-group $RESOURCE_GROUP \
    --storage-account $STORAGE_ACCOUNT \
    --app-insights $APP_INS \
    --app-insights-key $AI_KEY \
    --plan $LOGICAPPS_PLAN