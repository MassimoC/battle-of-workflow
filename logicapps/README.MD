# LogicApps 

```
sudo hwclock -s
az login --tenant "7517bc42-bcf8-4916-a677-b5753051f846" --use-device-code

# deploy infra

sh ./01-servicebus.sh

sh ./02-logicapps.sh
```

Change the runtime to ~4

https://techcommunity.microsoft.com/t5/integrations-on-azure-blog/azure-logic-apps-standard-now-supports-azure-functions-v4/ba-p/3656072


Build and deploy