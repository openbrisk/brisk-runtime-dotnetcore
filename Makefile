.PHONY: build

build:
	docker build . -t=openbrisk-runtime-dotnetcore

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