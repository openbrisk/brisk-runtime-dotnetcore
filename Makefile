.PHONY: build

build:
	docker build . -t=openbrisk-runtime-dotnetcore

bash:
	docker run -it \
	--entrypoint /bin/bash \
	-e MOD_NAME='HelloWorld' \
	-e FUNC_HANDLER='Execute' \
	-p 8080:8080 \
	-v `pwd`/examples:/openbrisk \
	openbrisk-runtime-dotnetcore

run:
	docker run -it \
	-e MOD_NAME='HelloWorld' \
	-e FUNC_HANDLER='Execute' \
	-p 8080:8080 \
	-v `pwd`/examples:/openbrisk \
	openbrisk-runtime-dotnetcore