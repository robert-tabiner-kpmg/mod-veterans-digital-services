#!/bin/bash
set -e

echo "============== INSTALLING CLOUD FOUNDRY CLI CLIENT =============="
# https://github.com/cloudfoundry/cli/releases
# wget --max-redirect=1 --output-document=cf_cli_6.26.0.tgz "https://cli.run.pivotal.io/stable?release=linux64-binary&version=7.1.0&source=github-rel"
wget --max-redirect=1 --output-document=cf_cli.tgz "https://cli.run.pivotal.io/stable?release=linux64-binary&source=github"
gunzip cf_cli.tgz
tar -xvf cf_cli.tar
ls

echo "============== SET CF Environment Variables =============="
./cf set-env blue Email__ApiKey cf_dev-1a5f7551-f80b-4e25-a949-f359fd367296-07cf4dec-07fd-484f-baf4-0261ed2378bc
./cf set-env blue Email__AFIPTemplateId f33a4803-399f-4b94-8a54-e9eec86437d7
./cf set-env blue Email__AFIPUserTemplateId 6a989172-ef95-451d-96a4-aa84738608e2
./cf set-env blue Email__AFCSTemplateId 10cf9af3-0e33-4bfd-8826-df136a95e209
./cf set-env blue Email__AFCSUserTemplateId 10cf9af3-0e33-4bfd-8826-df136a95e209
./cf set-env blue Email__AFCSEmailRecipient toby@codesure.co.uk
./cf set-env blue Redis__Uri $CF_REDIS_URL

echo "============== LOGGING INTO CLOUD FOUNDRY =============="
./cf login -a $CF_API -s $CF_SPACE -o $CF_ORGANIZATION -u $CF_USERNAME -p $CF_PASSWORD

echo "============== PUSHING CF APP =============="
./CF_DOCKER_PASSWORD=$DOCKER_PASSWORD cf push blue --docker-image modveterans/forms:dev --docker-username $DOCKER_USERNAME -u port

echo "============== BINDING REDIS AND S3 =============="
./cf create-service redis tiny-3.2 $REDIS_SERVICE_NAME
./cf create-service aws-s3-bucket tiny-3.2 $S3_SERVICE_NAME
./cf bind-service $APP_NAME $REDIS_SERVICE_NAME
./cf bind-service $APP_NAME $S3_SERVICE_NAME