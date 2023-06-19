#!/bin/bash
SID="c1537527-c126-428d-8f72-1ac9f2c63c1f"
az account set --subscription $SID

INFRACODE="workflowmma"
LOCATION="westeurope"
RESOURCE_GROUP="rg-${INFRACODE}"

#output values
OUT='\033[0;37m'
#debug
DBG='\033[0;32m'

#--------------------------------
#--- shared infra
#--------------------------------

# log analytics workspace
LOG_ANALYTICS_WORKSPACE="${INFRACODE}-law"

# application insights
APP_INS="${INFRACODE}-ain"

# service bus
SERVICEBUS_NAMESPACE="${INFRACODE}-bus"
SERVICEBUS_TOPIC="dapresb"

# azure container registry
REGISTRY="${INFRACODE}reg"

# azure storage account
STORAGE_ACCOUNT="${INFRACODE}sto"

# container registry
REGISTRY="${INFRACODE}reg"

# logic apps name
LOGICAPPS_NAME="${INFRACODE}-lap"

# logic apps plan
LOGICAPPS_PLAN="${INFRACODE}-pla"


WHITE='\033[0;37m' 
RED='\033[0;31m'
BLUE='\033[0;34m'
GREEN='\033[0;32m'
