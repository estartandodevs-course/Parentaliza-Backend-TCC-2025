# Dockerfile para AWS Lambda (.NET 9)
# Este Dockerfile é para build local. Para deployment real, use: dotnet lambda deploy-function

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj files e restaura dependências
COPY ["src/Parentaliza.API/Parentaliza.API.csproj", "src/Parentaliza.API/"]
COPY ["src/Parentaliza.Application/Parentaliza.Application.csproj", "src/Parentaliza.Application/"]
COPY ["src/Parentaliza.Domain/Parentaliza.Domain.csproj", "src/Parentaliza.Domain/"]
COPY ["Parentaliza.ServiceDefaults/Parentaliza.ServiceDefaults.csproj", "Parentaliza.ServiceDefaults/"]

RUN dotnet restore "src/Parentaliza.API/Parentaliza.API.csproj"

# Copia tudo e publish
COPY . .
WORKDIR "/src/src/Parentaliza.API"
RUN dotnet publish "Parentaliza.API.csproj" \
    -c Release \
    -o /app/publish

# Para produção, use Container Image com Lambda
FROM public.ecr.aws/lambda/dotnet:9
WORKDIR /var/task
COPY --from=build /app/publish .
# For .NET 9 with AddAWSLambdaHosting(LambdaEventSource.HttpApi),
# use the assembly entry point. The Program.cs Main method is the entry point.
# Handler format: AssemblyName::Namespace.Class::Method
CMD ["Parentaliza.API::LambdaEntryPoint::FunctionHandlerAsync"]

