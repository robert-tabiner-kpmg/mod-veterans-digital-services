#!/bin/bash
set -e

echo "============== INSTALLING CLOUD FOUNDRY CLI CLIENT =============="
mv ./ci/cf-cli.tgz ./cf_cli.tgz
gunzip cf_cli.tgz
tar -xvf cf_cli.tar

echo "============= LOGGING INTO CLOUD FOUNDRY ========================"
./cf login -a $CF_API -s $CF_SPACE -o $CF_ORGANIZATION -u $CF_USERNAME -p $CF_PASSWORD

echo "============= PUSHING CF APP ===================================="
./cf push $CF_APP_NAME --docker-image modveterans/forms:$DOCKER_TAG --docker-username $CF_DOCKER_USERNAME

echo "============= SET CF Environment Variables ======================"
./cf set-env $CF_APP_NAME Email__ApiKey $NOTIFY_API
./cf set-env $CF_APP_NAME Email__AFCSTemplateId $AFCS_TEMPLATE_ID
./cf set-env $CF_APP_NAME Email__AFCSUserTemplateId $AFCS_USER_TEMPLATE_ID
./cf set-env $CF_APP_NAME Email__AFCSEmailRecipient toby@codesure.co.uk
./cf set-env $CF_APP_NAME Redis__Uri $REDIS_URL

echo "============= BINDING REDIS AND S3 =============================="
#./cf create-service redis tiny-3.2 $REDIS_SERVICE_NAME
#./cf create-service aws-s3-bucket tiny-3.2 $S3_SERVICE_NAME
#./cf bind-service $CF_APP_NAME $REDIS_SERVICE_NAME
#./cf bind-service $CF_APP_NAME $S3_SERVICE_NAME