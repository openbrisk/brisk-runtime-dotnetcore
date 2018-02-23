.PHONY: build test bash run

build:
	docker build . -t=openbrisk-runtime-dotnetcore

start:
	dotnet restore --packages ./examples/packages ./examples/Hasher.csproj
	dotnet publish -c Release -o ./out ./examples/Hasher.csproj
	dotnet run --project ./src/OpenBrisk.Runtime/OpenBrisk.Runtime.csproj

bash:
	docker run -it \
	--entrypoint /bin/bash \
	-e MODULE_NAME='Hasher' \
	-e FUNCTION_HANDLER='Execute' \
	-p 8080:8080 \
	-v `pwd`/examples:/openbrisk \
	openbrisk-runtime-dotnetcore

run:
	docker run -it \
	-e MODULE_NAME='Hasher' \
	-e FUNCTION_HANDLER='Execute' \
	-p 8080:8080 \
	-v `pwd`/examples:/openbrisk \
	openbrisk-runtime-dotnetcore