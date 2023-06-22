
# Data model

```
cd simple-model
dotnet new classlib -o simple-model
```

# Receiver

```
cd simple-receive
dotnet new web
dotnet add package Dapr.Workflow
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package  Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
dotnet add package Dapr.Client 

dotnet add simple-receive.csproj reference ../simple-model/simple-model.csproj
```

make sure .dapr\components contains the component configuration


# Orchestration

```
cd  ../simple-workflow
dotnet new web
dotnet add package Dapr.Workflow
dotnet add package Dapr.Client 
dotnet add package Microsoft.Extensions.DependencyInjection

dotnet add simple-workflow.csproj reference ../simple-model/simple-model.csproj
```

```
# invoke a workflow

http://localhost:3622/v1.0-alpha1/workflows/dapr/Orchestration/start?instanceId=61269902-42f4-4b55-9bb5-7ee50a2d4d97

# status of a workflow instance
http://localhost:3622/v1.0-alpha1/workflows/dapr/ec07d527-bdbb-4470-84b7-0484f87df9ea

```


# Subscriber(s)

```
cd  ../simple-send
dotnet new web
dotnet add package Dapr.Client 

dotnet add simple-send.csproj reference ../simple-model/simple-model.csproj
```

# Add to the solution

```
cd .\battle-of-workflow\dapr\src
dotnet new sln
dotnet sln add ./simple-model/simple-model.csproj
dotnet sln add ./simple-receive/simple-receive.csproj
dotnet sln add ./simple-workflow/simple-workflow.csproj
dotnet sln add ./simple-send/simple-send.csproj
```


# Test


```
# RECEIVER
C:\GIT\GitHub\mmcr\battle-of-workflow\dapr\src\simple-receive
dapr run --app-id dreceiver --app-port 6002 --dapr-http-port 3602 dotnet run 

# WORKFLOW
C:\GIT\GitHub\mmcr\battle-of-workflow\dapr\src\simple-workflow
dapr run --app-id dworkflow --log-level error --app-port 5000 --dapr-http-port 3622 dotnet run

# SEND
C:\GIT\GitHub\mmcr\battle-of-workflow\dapr\src\simple-send
dapr run --app-id dsender --app-port 5168 --dapr-http-port 3612 dotnet run

```
